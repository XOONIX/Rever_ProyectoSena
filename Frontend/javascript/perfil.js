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
    
    // Función local para obtener la Payload del JWT sin librerías externas
    function obtenerPayloadJWT(jwtToken) {
        try {
            const base64Url = jwtToken.split('.')[1];
            const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
            const jsonPayload = decodeURIComponent(atob(base64).split('').map(c => 
                '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)
            ).join(''));
            return JSON.parse(jsonPayload);
        } catch (e) {
            return null;
        }
    }

    // Intentar leer idUsuario del localStorage o extraerlo directamente del JWT
    let idUsuario = localStorage.getItem('idUsuario');
    
    if (!idUsuario && token) {
        const payload = obtenerPayloadJWT(token);
        // Extraer la claim estándar 'nameidentifier' de Microsoft/XML
        idUsuario = payload?.["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
        if (idUsuario) {
            localStorage.setItem('idUsuario', idUsuario);
        }
    }

    // Validar que ambos existan
    if (!token || !idUsuario) {
        alert('Sesión no válida o expirada. Por favor, inicia sesión de nuevo.');
        window.location.href = 'login.html';
        return;
    }

    // Petición GET a la API con idUsuario y token...
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

        // Asignar datos al formulario
        if (document.getElementById('id_usuario')) document.getElementById('id_usuario').value = usuario.idUsuario || idUsuario;
        if (document.getElementById('nombre_completo')) document.getElementById('nombre_completo').value = usuario.nombre || '';
        if (document.getElementById('correo_electronico')) document.getElementById('correo_electronico').value = usuario.correo || '';
        if (document.getElementById('telefono_contacto')) document.getElementById('telefono_contacto').value = usuario.telefono || '';

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

    // Captura de valores limpios de espacios en blanco
    const nombre = document.getElementById('nombre_completo').value.trim();
    const correo = document.getElementById('correo_electronico').value.trim();
    const telefono = document.getElementById('telefono_contacto').value.trim();

    const contrasenaActual = document.getElementById('contrasena_actual').value;
    const nuevaContrasena = document.getElementById('nueva_contrasena').value;
    const confirmarNuevaContrasena = document.getElementById('confirmar_nueva_contrasena').value;

    // 1. Validar que los campos no estén vacíos
    if (!nombre) {
        alert('El nombre completo no puede estar vacío.');
        resetBotonSubmit(botonSubmit);
        return;
    }

    if (!correo) {
        alert('El correo electrónico es obligatorio.');
        resetBotonSubmit(botonSubmit);
        return;
    }

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(correo)) {
        alert('Por favor ingresa un correo electrónico válido.');
        resetBotonSubmit(botonSubmit);
        return;
    }

    if (!telefono) {
        alert('El número de teléfono de contacto es obligatorio.');
        resetBotonSubmit(botonSubmit);
        return;
    }

    if (telefono.length < 7) {
        alert('Ingresa un número de teléfono válido (mínimo 7 dígitos).');
        resetBotonSubmit(botonSubmit);
        return;
    }

    // 2. Validar actualización de contraseña si se ingresó alguna
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

        if (!contrasenaActual) {
            alert('Debes ingresar tu contraseña actual para confirmar el cambio.');
            resetBotonSubmit(botonSubmit);
            return;
        }
    }

    // 3. Recuperar ID del usuario
    const idUsuario = document.getElementById('id_usuario')?.value || localStorage.getItem('idUsuario');

    if (!idUsuario || parseInt(idUsuario) <= 0) {
        alert('Error: No se encontró el ID del usuario actual.');
        resetBotonSubmit(botonSubmit);
        return;
    }

    // 4. Crear DTO para el backend
    const datos = {
        idUsuario: parseInt(idUsuario),
        nombre: nombre,
        correo: correo,
        telefono: telefono,
        contraseña: nuevaContrasena || "",
        idRol: parseInt(document.getElementById('tipo_usuario')?.value || localStorage.getItem('idRol') || "2")
    };

    const token = localStorage.getItem('token');

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
            throw new Error(errorTexto || 'Error al actualizar la información en la base de datos.');
        }

        let usuarioActualizado = {};
        const contentType = response.headers.get("content-type");
        if (contentType && contentType.includes("application/json")) {
            usuarioActualizado = await response.json();
        }

        console.log('Respuesta backend:', usuarioActualizado);
        alert('¡Perfil actualizado con éxito!');
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

async function eliminar_cuenta() {
    const confirmacion = confirm('¿Estás seguro de que deseas eliminar tu cuenta? Esta acción no se puede deshacer.');
    if (!confirmacion) return;

    const token = localStorage.getItem('token');
    const idUsuario = localStorage.getItem('idUsuario');

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

        alert('Tu cuenta ha sido eliminada.');
        localStorage.clear();
        window.location.href = 'login.html';

    } catch (error) {
        alert('Error: ' + error.message);
        console.error('Error al eliminar la cuenta:', error);
    }
}