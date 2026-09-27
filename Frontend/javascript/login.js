document.querySelector('.formulario_login').addEventListener('submit', async (e) => {
    e.preventDefault();

    const botonSubmit = e.target.querySelector('button[type="submit"]');
    botonSubmit.disabled = true;
    botonSubmit.textContent = 'Ingresando...';

    const datos = {
        correo: document.getElementById('correo_electronico').value,
        contraseña: document.getElementById('contrasena').value
    };

    try {
        const response = await fetch('https://localhost:7015/api/Auth/Login', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(datos)
        });

        if (!response.ok) {
            const mensajeError = await response.text();
            throw new Error(mensajeError || 'Correo o contraseña incorrectos');
        }

        // 1. Primero conviertes la respuesta en JSON para sacar el token
        const resultado = await response.json();

        // 2. Guardas el token ANTES de intentar decodificarlo
        localStorage.setItem('sesion_activa', 'true');
        localStorage.setItem('token', resultado.token);

        // 3. Ahora sí, obtener_usuario_actual() puede leer el token recién guardado
        const usuario = obtener_usuario_actual();

        // Mapa de rutas por ID de Rol (según tabla roles: 1=administrador, 2=vendedor, 3=comprador)
        const rutas = {
            1: 'panel_administrador.html', // administrador
            2: 'publicar_inmueble.html',   // vendedor
            3: 'index.html'                // comprador
        };

        // Redirección directa
        window.location.href = rutas[usuario?.idRol] || 'index.html';

    } catch (error) {
        mostrar_notificacion(error.message, 'error');
        console.error(error);
        botonSubmit.disabled = false;
        botonSubmit.textContent = 'Iniciar Sesión';
    }
});
document.addEventListener('DOMContentLoaded', () => {
    const urlParams = new URLSearchParams(window.location.search);

    if (urlParams.get('confirmed') === 'true') {
        mostrar_notificacion('¡Tu cuenta ha sido activada con éxito! Ya puedes iniciar sesión.', 'exito');
        window.history.replaceState({}, document.title, window.location.pathname);
    } else if (urlParams.get('error') === 'invalid_token') {
        mostrar_notificacion('El enlace de confirmación no es válido o ya expiró.', 'error');
        window.history.replaceState({}, document.title, window.location.pathname);
    }
});