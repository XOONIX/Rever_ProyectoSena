// ==========================================
// 1. FUNCIONES AUXILIARES DE LA INTERFAZ
// ==========================================

// Restaurar el estado inicial del botón submit
function resetBotonSubmit(boton) {
    if (boton) {
        boton.disabled = false;
        boton.textContent = 'Guardar Cambios';
    }
}

// Navegación
function volver_a_inicio() {
    window.location.href = 'index.html';
}

function cancelar() {
    window.location.reload();
}

// Alternar visibilidad de los campos de contraseña (tipo password <-> text)
function alternar_visibilidad_contrasena(idInput, boton) {
    const input = document.getElementById(idInput);
    if (!input) return;

    if (input.type === 'password') {
        input.type = 'text';
        boton.classList.add('visible');
    } else {
        input.type = 'password';
        boton.classList.remove('visible');
    }
}

// Manejo de la selección e previsualización de la foto de perfil
function abrir_selector_imagen() {
    document.getElementById('input_imagen_perfil')?.click();
}

function manejar_cambio_imagen(event) {
    const archivo = event.target.files[0];
    if (archivo) {
        const lector = new FileReader();
        lector.onload = function (e) {
            const imgPerfil = document.getElementById('imagen_perfil_actual');
            if (imgPerfil) {
                imgPerfil.src = e.target.result;
            }
        };
        lector.readAsDataURL(archivo);
    }
}


// ==========================================
// 2. CARGA INICIAL DE DATOS DEL USUARIO (GET)
// ==========================================

document.addEventListener('DOMContentLoaded', async function () {
    const token = localStorage.getItem('token');

    // Si no hay token guardado, redirigir
    if (!token) {
        alert('Sesión no válida o expirada. Por favor, inicia sesión de nuevo.');
        window.location.href = 'login.html';
        return;
    }

    // Función auxiliar en memoria para leer la payload del JWT
    function obtenerIdUsuarioDesdeToken(jwtToken) {
        try {
            const base64Url = jwtToken.split('.')[1];
            const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
            const jsonPayload = decodeURIComponent(atob(base64).split('').map(c => 
                '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)
            ).join(''));
            
            const payload = JSON.parse(jsonPayload);
            // Mapea el claim nameidentifier de Microsoft
            return payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
        } catch (e) {
            return null;
        }
    }

    // Extraer el ID solo en la memoria del script (nunca se guarda en localStorage)
    const idUsuario = obtenerIdUsuarioDesdeToken(token);

    if (!idUsuario) {
        alert('El token de sesión no es válido. Por favor, inicia sesión nuevamente.');
        window.location.href = 'login.html';
        return;
    }

    // Petición a la API usando el ID extraído
    try {
        const response = await fetch(`https://localhost:7015/api/Usuario/${idUsuario}`, {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        });

        if (!response.ok) {
            const errorTexto = await response.text();
            throw new Error(errorTexto || 'No se pudieron cargar los datos del perfil.');
        }

        const usuario = await response.json();

        // Rellenar el formulario
        if (document.getElementById('id_usuario')) document.getElementById('id_usuario').value = usuario.idUsuario || idUsuario;
        if (document.getElementById('nombre_completo')) document.getElementById('nombre_completo').value = usuario.nombre || '';
        if (document.getElementById('correo_electronico')) document.getElementById('correo_electronico').value = usuario.correo || '';
        if (document.getElementById('telefono_contacto')) document.getElementById('telefono_contacto').value = usuario.telefono || '';
        if (document.getElementById('tipo_usuario') && usuario.idRol) document.getElementById('tipo_usuario').value = usuario.idRol;
        if (document.getElementById('imagen_perfil_actual') && usuario.imagenPerfil) document.getElementById('imagen_perfil_actual').src = usuario.imagenPerfil;

    } catch (error) {
        alert('Error: ' + error.message);
        console.error('Error al obtener datos del usuario:', error);
    }
});

// ==========================================
// 3. ENVÍO DEL FORMULARIO Y ACTUALIZACIÓN (PUT)
// ==========================================

document.getElementById('formulario_perfil')?.addEventListener('submit', async function (e) {
    e.preventDefault();

    const botonSubmit = e.target.querySelector('button[type="submit"]');
    botonSubmit.disabled = true;
    botonSubmit.textContent = 'Guardando...';

    // Captura de datos básicos
    const nombre = document.getElementById('nombre_completo').value.trim();
    const correo = document.getElementById('correo_electronico').value.trim();
    const telefono = document.getElementById('telefono_contacto').value.trim();
    // Campos de contraseña opcionales
    const nuevaContrasena = document.getElementById('nueva_contrasena').value;
    const confirmarNuevaContrasena = document.getElementById('confirmar_nueva_contrasena').value;

    // 1. Validaciones básicas de campos obligatorios
    if (!nombre) {
        alert('El nombre completo no puede estar vacío.');
        resetBotonSubmit(botonSubmit);
        return;
    }

    if (!telefono || telefono.length < 7) {
        alert('Por favor ingresa un número de teléfono válido.');
        resetBotonSubmit(botonSubmit);
        return;
    }

    // 2. Validación de cambio de contraseña sólo si se escribió algo
    if (nuevaContrasena || confirmarNuevaContrasena) {
        if (nuevaContrasena !== confirmarNuevaContrasena) {
            alert('Las nuevas contraseñas no coinciden.');
            resetBotonSubmit(botonSubmit);
            return;
        }

        if (nuevaContrasena.length < 6) {
            alert('La nueva contraseña debe tener al menos 6 caracteres.');
            resetBotonSubmit(botonSubmit);
            return;
        }
    }

    // 3. Obtener el ID del usuario directamente desde el token en memoria
    const token = localStorage.getItem('token');
    const idUsuario = document.getElementById('id_usuario')?.value || obtenerIdUsuarioDesdeToken(token);

    if (!idUsuario) {
        alert('Error al identificar al usuario. Inicia sesión nuevamente.');
        resetBotonSubmit(botonSubmit);
        return;
    }

    // 4. Estructurar DTO enviando la nueva contraseña o vacía si no se modificó
    const datos = {
        idUsuario: parseInt(idUsuario),
        nombre: nombre,
        correo: correo,
        telefono: telefono,
        contraseña: nuevaContrasena ? nuevaContrasena : "", 
        idRol: parseInt(document.getElementById('tipo_usuario')?.value || "2")
    };

    try {
        const response = await fetch('https://localhost:7015/api/Usuario', {
            method: 'PUT',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(datos)
        });

        if (!response.ok) {
            const errorTexto = await response.text();
            throw new Error(errorTexto || 'Error al actualizar la información.');
        }

        alert('¡Perfil actualizado correctamente!');
        window.location.href = 'index.html';

    } catch (error) {
        alert('Error: ' + error.message);
        console.error('Error durante la actualización:', error);
        resetBotonSubmit(botonSubmit);
    }
});

// ==========================================
// 4. ZONA DE PELIGRO (ELIMINAR CUENTA)
// ==========================================

function eliminar_cuenta() {
    const modal = document.getElementById('modal_confirmacion');
    const inputConfirmacion = document.getElementById('modal_input_confirmacion');
    const btnCancelar = document.getElementById('modal_btn_cancelar');
    const btnConfirmar = document.getElementById('modal_btn_confirmar');

    // Limpiar input y mostrar modal
    inputConfirmacion.value = '';
    modal.classList.add('activo');

    // Ocultar al hacer clic en cancelar
    btnCancelar.onclick = () => {
        modal.classList.remove('activo');
    };

    // Procesar confirmación
    btnConfirmar.onclick = async () => {
        if (inputConfirmacion.value.trim() !== 'ELIMINAR') {
            alertarPersonalizado('Debes escribir la palabra ELIMINAR para continuar.');
            return;
        }

        modal.classList.remove('activo');

        const token = localStorage.getItem('token');
        const idUsuario = document.getElementById('id_usuario')?.value || obtenerIdUsuarioDesdeToken(token);

        if (!token || !idUsuario) {
            window.location.href = 'login.html';
            return;
        }

        try {
            const response = await fetch(`https://localhost:7015/api/Usuario/${idUsuario}`, {
                method: 'DELETE',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                }
            });

            if (!response.ok) {
                const errorTexto = await response.text();
                throw new Error(errorTexto || 'No se pudo eliminar la cuenta.');
            }

            localStorage.clear();
            window.location.href = 'login.html';

        } catch (error) {
            console.error('Error al eliminar la cuenta:', error);
            alertarPersonalizado('Error: ' + error.message);
        }
    };
}