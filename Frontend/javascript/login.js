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

    // 1. Convertir la respuesta en JSON para sacar el token
    const resultado = await response.json();

    // 2. Guardar el token en el almacenamiento local
    localStorage.setItem('sesion_activa', 'true');
    localStorage.setItem('token', resultado.token);

    // 3. Obtener los datos del usuario usando el token guardado
    const usuario = obtener_usuario_actual();

    // 4. Lógica de redirección en caso de venir de una pagina de inmueble
    const params = new URLSearchParams(window.location.search);
    const volver_a = params.get('volver');

    if (volver_a) {
        window.location.href = decodeURIComponent(volver_a);
    } else {
        const rutas = {
            1: 'panel_administrador.html', // administrador
            2: 'publicar_inmueble.html',   // vendedor
            3: 'index.html'                // comprador
        };
        window.location.href = rutas[usuario?.idRol] || 'index.html';
    }

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


function mostrar_inicio_sesion_gmail() {
  mostrar_notificacion('Inicio de sesión con Google (función en construcción)', 'info');
}
function mostrar_olvidaste_contrasena() {
  mostrar_notificacion('Función de recuperación de contraseña (función en construcción)', 'info');
}