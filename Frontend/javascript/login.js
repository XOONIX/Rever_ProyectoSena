
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

            // Desestructuración limpia de la respuesta de la API
            const { token, usuario } = await response.json();

            // Guardar datos de sesión
            localStorage.setItem('sesion_activa', 'true');
            localStorage.setItem('token', token);
            localStorage.setItem('usuario_actual', JSON.stringify(usuario));

            // Mapa de rutas por ID de Rol
            const rutas = { 
                1: 'panel_administrador.html', 
                2: 'publicar_inmueble.html', 
                3: 'index.html' 
            };

            // Redirección directa
            window.location.href = rutas[usuario?.idRol] || 'index.html';

        } catch (error) {
            alert('Error: ' + error.message);
            console.error(error);
            botonSubmit.disabled = false;
            botonSubmit.textContent = 'Iniciar Sesión';
        }
    });