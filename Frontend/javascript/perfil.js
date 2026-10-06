// Función para alternar visibilidad de contraseña
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

        // Función para abrir selector de imagen
        function abrir_selector_imagen() {
            document.getElementById('input_imagen_perfil').click();
        }

        // Función para manejar cambio de imagen
        function manejar_cambio_imagen(evento) {
            const archivo = evento.target.files[0];
            if (archivo) {
                const reader = new FileReader();
                reader.onload = function(e) {
                    document.getElementById('imagen_perfil_actual').src = e.target.result;
                };
                reader.readAsDataURL(archivo);
            }
        }

        // Función para volver al inicio
        function volver_a_inicio() {
            window.location.href = 'index.html';
        }

        // Función para cancelar
        function cancelar() {
            if (confirm('¿Estás seguro de que deseas cancelar? Los cambios no guardados se perderán.')) {
                volver_a_inicio();
            }
        }

        // Función para eliminar cuenta
        function eliminar_cuenta() {
            if (confirm('¿Estás seguro de que deseas eliminar tu cuenta? Esta acción es irreversible y todos tus datos serán eliminados permanentemente.')) {
                alert('Función de eliminación de cuenta - pendiente de implementación con backend');
            }
        }

        // Cargar datos del usuario al iniciar
        window.addEventListener('DOMContentLoaded', function() {
            // Simular carga de datos del usuario desde localStorage o API
            const usuarioGuardado = localStorage.getItem('usuario_actual');
            if (usuarioGuardado) {
                const usuario = JSON.parse(usuarioGuardado);
                document.getElementById('nombre_completo').value = usuario.nombre || '';
                document.getElementById('correo_electronico').value = usuario.correo || '';
                document.getElementById('telefono_contacto').value = usuario.telefono || '';
                document.getElementById('tipo_usuario').value = usuario.idRol || '2';
                
                if (usuario.imagen_perfil) {
                    document.getElementById('imagen_perfil_actual').src = usuario.imagen_perfil;
                }
            }
        });

        // Manejar envío del formulario
        document.getElementById('formulario_perfil').addEventListener('submit', async function(e) {
            e.preventDefault();

            const botonSubmit = e.target.querySelector('button[type="submit"]');
            botonSubmit.disabled = true;
            botonSubmit.textContent = 'Guardando...';

            const contrasenaActual = document.getElementById('contrasena_actual').value;
            const nuevaContrasena = document.getElementById('nueva_contrasena').value;
            const confirmarNuevaContrasena = document.getElementById('confirmar_nueva_contrasena').value;

            // Validar contraseñas si se están cambiando
            if (nuevaContrasena || confirmarNuevaContrasena) {
                if (nuevaContrasena !== confirmarNuevaContrasena) {
                    alert('Las nuevas contraseñas no coinciden.');
                    botonSubmit.disabled = false;
                    botonSubmit.textContent = 'Guardar Cambios';
                    return;
                }
            }

            const datos = {
                nombre: document.getElementById('nombre_completo').value,
                correo: document.getElementById('correo_electronico').value,
                telefono: document.getElementById('telefono_contacto').value,
                contrasenaActual: contrasenaActual,
                nuevaContrasena: nuevaContrasena,
                imagen_perfil: document.getElementById('imagen_perfil_actual').src
            };

            try {
                // Simular llamada al API
                // const response = await fetch('https://localhost:7015/api/Perfil/Actualizar', {
                //     method: 'PUT',
                //     headers: { 'Content-Type': 'application/json' },
                //     body: JSON.stringify(datos)
                // });

                // Simulación exitosa
                alert('¡Perfil actualizado con éxito!');
                
                // Guardar datos actualizados en localStorage
                localStorage.setItem('usuario_actual', JSON.stringify({
                    ...JSON.parse(localStorage.getItem('usuario_actual') || '{}'),
                    ...datos
                }));

                // Actualizar avatar en la página principal si existe
                localStorage.setItem('imagen_avatar', datos.imagen_perfil);

                window.location.href = 'index.html';

            } catch (error) {
                alert('Error: ' + error.message);
                console.error(error);
                botonSubmit.disabled = false;
                botonSubmit.textContent = 'Guardar Cambios';
            }
        });