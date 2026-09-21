
function alternar_visibilidad_contrasena(id_campo, boton) {
    var campo = document.getElementById(id_campo);
    var icono = boton.querySelector('.icono_ojo');
    
    if (campo.type === 'password') {
        campo.type = 'text';
        icono.innerHTML = '<path d="M12 7c2.76 0 5 2.24 5 5 0 .65-.13 1.26-.36 1.83l2.92 2.92c1.51-1.26 2.7-2.89 3.43-4.75-1.73-4.39-6-7.5-11-7.5-1.4 0-2.74.25-3.98.7l2.16 2.16C10.74 7.13 11.35 7 12 7zM2 4.27l2.28 2.28.46.46C3.08 8.3 1.78 10.02 1 12c1.73 4.39 6 7.5 11 7.5 1.55 0 3.03-.3 4.38-.84l.42.42L19.73 22 21 20.73 3.27 3 2 4.27zM7.53 9.8l1.55 1.55c-.05.21-.08.43-.08.65 0 1.66 1.34 3 3 3 .22 0 .44-.03.65-.08l1.55 1.55c-.67.33-1.41.53-2.2.53-2.76 0-5-2.24-5-5 0-.79.2-1.53.53-2.2zm4.31-.78l3.15 3.15.02-.16c0-1.66-1.34-3-3-3l-.17.01z"/>';
    } else {
        campo.type = 'password';
        icono.innerHTML = '<path d="M12 4.5C7 4.5 2.73 7.61 1 12c1.73 4.39 6 7.5 11 7.5s9.27-3.11 11-7.5c-1.73-4.39-6-7.5-11-7.5zM12 17c-2.76 0-5-2.24-5-5s2.24-5 5-5 5 2.24 5 5-2.24 5-5 5zm0-8c-1.66 0-3 1.34-3 3s1.34 3 3 3 3-1.34 3-3-1.34-3-3-3z"/>';
    }
}

const RUTAS_POR_ROL = {
    1: 'panel_admin.html',
    2: 'index.html',
    3: 'panel_vendedor.html',
};

document.getElementById('formulario_registro').addEventListener('submit', async function(e) {
    e.preventDefault();

    const botonSubmit = e.target.querySelector('button[type="submit"]');
    botonSubmit.disabled = true;
    botonSubmit.textContent = 'Registrando...';

    const contrasena = document.getElementById('contrasena').value;
    const confirmarContrasena = document.getElementById('confirmar_contrasena').value;

    if (contrasena !== confirmarContrasena) {
        mostrar_notificacion('Las contraseñas no coinciden.', 'error');
        botonSubmit.disabled = false;
        botonSubmit.textContent = 'Registrarse';
        return;
    }

    const correo = document.getElementById('correo_electronico').value;

    const datosRegistro = {
        nombre: document.getElementById('nombre_completo').value,
        correo: correo,
        telefono: document.getElementById('telefono_contacto').value,
        contraseña: contrasena,
        idRol: parseInt(document.getElementById('tipo_usuario').value)
    };

    try {
        // 1. Registrar el usuario
        const respuestaRegistro = await fetch('https://localhost:7015/api/Usuario', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(datosRegistro)
        });

        const textoRegistro = await respuestaRegistro.text();
        let mensajeRegistro = textoRegistro;
        try {
            const json = JSON.parse(textoRegistro);
            mensajeRegistro = json.title || json.message || JSON.stringify(json);
        } catch {
            // texto plano, se queda igual
        }

        if (!respuestaRegistro.ok) {
            throw new Error(mensajeRegistro || 'Error al registrar usuario');
        }

        // 2. Iniciar sesión automáticamente con las mismas credenciales
        const respuestaLogin = await fetch('https://localhost:7015/api/Auth/Login', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ correo: correo, contraseña: contrasena })
        });

        if (!respuestaLogin.ok) {
            // El registro sí funcionó, solo el auto-login falló — mándalo a loguearse a mano
            mostrar_notificacion('¡Cuenta creada! Ahora inicia sesión.', 'exito');
            setTimeout(() => { window.location.href = 'login.html'; }, 1200);
            return;
        }

        const resultadoLogin = await respuestaLogin.json();

        localStorage.setItem('sesion_activa', 'true');
        localStorage.setItem('token', resultadoLogin.token);
        localStorage.setItem('usuario_actual', JSON.stringify(resultadoLogin.usuario));

        mostrar_notificacion('¡Cuenta creada con éxito!', 'exito');
        setTimeout(() => {
            window.location.href = RUTAS_POR_ROL[resultadoLogin.usuario.idRol] || 'index.html';
        }, 1200);

    } catch (error) {
        mostrar_notificacion(error.message, 'error');
        console.error(error);
        botonSubmit.disabled = false;
        botonSubmit.textContent = 'Registrarse';
    }
});