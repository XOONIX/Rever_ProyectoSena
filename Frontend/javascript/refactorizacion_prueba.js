 function setStatus(status) {
        // Remover clase activa de todos
        document.querySelectorAll('.status-option').forEach(opt => {
            opt.classList.remove('active');
        });
        
        // Agregar clase activa al seleccionado
        const selectedOpt = document.querySelector(`.status-option[data-status="${status}"]`);
        if (selectedOpt) {
            selectedOpt.classList.add('active');
        }
        
        // Actualizar el valor del input oculto
        document.getElementById('propertyStatus').value = status;
    }

    // Activar/desactivar campos de tiempo según el checkbox del día
    document.querySelectorAll('.schedule-row input[type="checkbox"]').forEach(checkbox => {
        checkbox.addEventListener('change', function() {
            const row = this.closest('.schedule-row');
            const timeInputs = row.querySelectorAll('input[type="time"]');
            timeInputs.forEach(input => {
                input.disabled = !this.checked;
            });
        });
    });

    // Manejo del envío del formulario (ejemplo)
    document.getElementById('editPropertyForm').addEventListener('submit', function(e) {
        e.preventDefault();
        const formData = new FormData(this);
        alert('¡Cambios guardados con éxito! Estado actual: ' + formData.get('status').toUpperCase());
        // Aquí integrarías el fetch/axios hacia tu API backend.
    });

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

document.getElementById('formulario_registro').addEventListener('submit', async function (e) {
    e.preventDefault();

    const botonSubmit = e.target.querySelector('button[type="submit"]');
    botonSubmit.disabled = true;
    botonSubmit.textContent = 'Registrando...';

    const contrasena = document.getElementById('contrasena').value;
    const confirmarContrasena = document.getElementById('confirmar_contrasena').value;
    const aceptarDatos = document.getElementById('aceptar_datos').checked;

    if (contrasena !== confirmarContrasena) {
        mostrar_notificacion('Las contraseñas no coinciden.', 'error');
        botonSubmit.disabled = false;
        botonSubmit.textContent = 'Registrarse';
        return;
    }

    if (!aceptarDatos) {
        mostrar_notificacion('Debes aceptar el tratamiento de datos personales para continuar.', 'error');
        botonSubmit.disabled = false;
        botonSubmit.textContent = 'Registrarse';
        return;
    }

    const datosRegistro = {
        nombre: document.getElementById('nombre_completo').value,
        correo: document.getElementById('correo_electronico').value,
        telefono: document.getElementById('telefono_contacto').value,
        contraseña: contrasena,
        idRol: parseInt(document.getElementById('tipo_usuario').value)
    };

    try {
        const respuesta = await fetch('https://localhost:7015/api/Auth/Register', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(datosRegistro)
        });

        const texto = await respuesta.text();
        let mensaje = texto;
        try {
            const json = JSON.parse(texto);
            mensaje = json.message || json.title || JSON.stringify(json);
        } catch {
        }

        if (!respuesta.ok) {
            throw new Error(mensaje || 'Error al registrar usuario');
        }

        // Mostrar notificación
        mostrar_notificacion('¡Registro exitoso! Revisa tu correo para confirmar tu cuenta.', 'exito');

    } catch (error) {
        mostrar_notificacion(error.message, 'error');
        console.error(error);
        botonSubmit.disabled = false;
        botonSubmit.textContent = 'Registrarse';
    }
});

/* ═══════════════════════════════════════════════════════════════
   REVER INMOBILIARIA — Pantalla de Detalle de Propiedad
   Nomenclatura: Español + snake_case
   Autor: Rever Inmobiliaria © 2026
═══════════════════════════════════════════════════════════════ */

'use strict';

/* ════════════════════════════════════════════════════════════
   CONFIGURACIÓN & ESTADO GLOBAL
════════════════════════════════════════════════════════════ */

const estado_detalle = {
  propiedad_actual: null,    /* Objeto propiedad cargado desde la API */
  indice_galeria:   0,       /* Foto activa en el modal */
  es_favorito:      false,
  /* Estado del calendario de visita */
  calendario: {
    fecha_actual: new Date(),
    mes_actual: new Date(),
    dia_seleccionado: null,
    hora_seleccionada: null,
  },
};


/* ════════════════════════════════════════════════════════════
   UTILIDADES
════════════════════════════════════════════════════════════ */

// Intercepta "Llamar" y "Email" mientras la función está en desarrollo.
// Usa delegación: funciona aunque los enlaces se creen o recreen después.
document.addEventListener('click', (evento) => {
  const enlace = evento.target.closest('#enlace_llamar, #enlace_email');
  if (!enlace) return;

  evento.preventDefault(); // evita abrir tel: / mailto:

  const mensaje = enlace.id === 'enlace_llamar'
    ? 'La opción de llamar está en progreso'
    : 'La opción de email está en progreso';

  mostrar_aviso_en_progreso(mensaje);
});



/**
 * Obtiene un elemento por ID de forma segura.
 * @param {string} id
 * @returns {HTMLElement|null}
 */
function obtener_elemento_detalle(id) {
  return document.getElementById(id);
}

/**
 * Muestra una notificación temporal.
 * @param {string} mensaje
 * @param {'exito'|'error'|'info'} tipo
 */

/**
 * Lee el parámetro `id` de la URL (?id=1).
 * @returns {number|null}
 */
function obtener_id_desde_url() {
  const params = new URLSearchParams(window.location.search);
  const id = parseInt(params.get('id'), 10);
  return isNaN(id) ? null : id;
}


/* ════════════════════════════════════════════════════════════
   CARGA DE DATOS DESDE LA API
════════════════════════════════════════════════════════════ */

/**
 * Consulta el backend para traer el detalle de la propiedad.
 */
async function cargar_detalle_inmueble() {
  const id = obtener_id_desde_url();
  if (!id) {
    mostrar_notificacion_detalle('No se especificó ningún inmueble válido', 'error');
    return;
  }

  try {
    const respuesta = await fetch(`${API_URL}/Inmueble/${id}/Detalle`);
    if (!respuesta.ok) throw new Error('No se pudo cargar la propiedad');

    const inmueble = await respuesta.json();
    renderizar_detalle(inmueble);

    const usuario_actual = obtener_usuario_actual();
    const texto_usuario_contacto = document.getElementById('texto_usuario_contacto');
    if (usuario_actual && texto_usuario_contacto) {
        texto_usuario_contacto.textContent = `${usuario_actual.nombre} (${usuario_actual.correo})`;
    }
    actualizar_estado_formulario_contacto();

  } catch (error) {
    console.error('Error al cargar la propiedad:', error);
    mostrar_notificacion_detalle('No se pudo cargar la información del inmueble', 'error');
  } finally {
    const overlay = obtener_elemento_detalle('overlay_carga');
    const pantalla = obtener_elemento_detalle('pantalla_detalle');
    if (overlay) overlay.classList.add('oculto');
    if (pantalla) pantalla.classList.remove('oculto');
  }
}


/* ════════════════════════════════════════════════════════════
   RENDERIZADO DEL DETALLE
════════════════════════════════════════════════════════════ */

/**
 * Monta toda la pantalla de detalle con el objeto recibido del backend.
 * @param {Object} prop
 */
function renderizar_detalle(prop) {
  estado_detalle.propiedad_actual = prop;

  // Garantizar array de imágenes
  const imagenes = (prop.imagenes && prop.imagenes.length > 0)
    ? prop.imagenes
    : ['https://via.placeholder.com/800x600?text=Sin+imagen'];

  /* ── Hero ── */
  const img_hero = obtener_elemento_detalle('imagen_hero');
  if (img_hero) {
    img_hero.src = imagenes[0];
    img_hero.alt = prop.titulo;
    img_hero.onclick = () => abrir_galeria(0);
  }

  /* Badges del hero */
  const badge_modo = obtener_elemento_detalle('badge_modo_hero');
  if (badge_modo) {
    const modo_texto = prop.modo ? (prop.modo.charAt(0).toUpperCase() + prop.modo.slice(1)) : 'Venta';
    badge_modo.textContent = modo_texto;
    badge_modo.className = 'badge_modo_hero ' + (prop.modo || 'venta');
  }

  const badge_cat = obtener_elemento_detalle('badge_categoria_hero');
  if (badge_cat) {
    if (prop.tipo || prop.badge) {
      badge_cat.textContent = prop.tipo || prop.badge;
      badge_cat.classList.remove('oculto');
    } else {
      badge_cat.classList.add('oculto');
    }
  }

  /* Botón ver galería */
  const texto_galeria = obtener_elemento_detalle('texto_ver_galeria');
  if (texto_galeria) {
    texto_galeria.textContent = `Ver todas las fotos (${imagenes.length})`;
  }

  /* ── Miniaturas ── */
  renderizar_miniaturas(imagenes);

  /* ── Título y dirección ── */
  const titulo = obtener_elemento_detalle('titulo_propiedad');
  if (titulo) {
    titulo.textContent = prop.titulo;
    document.title = `${prop.titulo} — Rever Inmobiliaria`;
  }

  const texto_dir = obtener_elemento_detalle('texto_direccion');
  if (texto_dir) texto_dir.textContent = prop.direccion;

  const zona = obtener_elemento_detalle('zona_propiedad');
  if (zona) zona.textContent = prop.ubicacion;

  /* ── Chips de specs ── */
  renderizar_specs(prop);

  /* ── Descripción ── */
  const desc = obtener_elemento_detalle('texto_descripcion');
  if (desc) desc.textContent = prop.descripcion || 'Sin descripción disponible.';

  /* ── Características ── */
  renderizar_caracteristicas(prop.caracteristicas || []);

  /* ── Galería en grilla ── */
  renderizar_grilla_galeria(imagenes);

  /* ── Pin del mapa ── */
  const zona_pin = obtener_elemento_detalle('zona_pin_mapa');
  if (zona_pin) zona_pin.textContent = prop.ubicacion;

  /* ── Tarjeta contacto ── */
  renderizar_contacto(prop);

  /* Mensaje inicial del textarea */
  const textarea = obtener_elemento_detalle('input_mensaje_contacto');

  /* Restaurar estado de favoritos */
  restaurar_estado_favorito(prop.idInmueble || prop.id);
}

/**
 * Renderiza las miniaturas de fotos adicionales bajo el hero.
 * @param {string[]} imagenes
 */
function renderizar_miniaturas(imagenes) {
  const contenedor = obtener_elemento_detalle('lista_miniaturas');
  if (!contenedor) return;

  contenedor.innerHTML = imagenes.map((img, i) => `
    <div class="miniatura_foto" role="listitem">
      <img
        src="${img}"
        alt="Vista ${i + 1} de la propiedad"
        loading="lazy"
        onclick="cambiar_imagen_hero(${i})"
      />
    </div>
  `).join('');
}

/**
 * Cambia la imagen visible en el hero principal.
 * @param {number} indice
 */
function cambiar_imagen_hero(indice) {
  const prop = estado_detalle.propiedad_actual;
  if (!prop || !prop.imagenes || !prop.imagenes[indice]) return;

  const img_hero = obtener_elemento_detalle('imagen_hero');
  if (img_hero) {
    img_hero.src = prop.imagenes[indice];
  }
}

/**
 * Renderiza los chips de especificaciones.
 * @param {Object} prop
 */
function renderizar_specs(prop) {
  const contenedor = obtener_elemento_detalle('grilla_specs');
  if (!contenedor) return;

  const specs = [];

  const hab = prop.habitaciones ?? prop.hab;
  if (hab !== undefined && hab > 0) {
    specs.push({ icono: '🛏️', valor: hab, etiqueta: 'Habitaciones' });
  }

  const banos = prop.banos;
  if (banos !== undefined) {
    specs.push({ icono: '🚿', valor: banos, etiqueta: 'Baños' });
  }

  const parqueaderos = prop.parqueaderos;
  if (parqueaderos !== undefined && parqueaderos > 0) {
    specs.push({ icono: '🚗', valor: parqueaderos, etiqueta: 'Parqueaderos' });
  }

  const area = prop.metrosCuadrados ?? prop.area;
  if (area) {
    specs.push({ icono: '📐', valor: prop.area_str || `${area} m²`, etiqueta: 'Área' });
  }

  if (prop.estrato) {
    specs.push({ icono: '📊', valor: prop.estrato, etiqueta: 'Estrato' });
  }

  if (prop.tipo) {
    specs.push({ icono: '🏠', valor: prop.tipo, etiqueta: 'Tipo' });
  }

  if (prop.piso && prop.piso > 1) {
    specs.push({ icono: '🏢', valor: `${prop.piso}°`, etiqueta: 'Piso' });
  }

  if (prop.antiguedad !== undefined) {
    specs.push({
      icono: '📅',
      valor: prop.antiguedad === 0 ? 'Estreno' : `${prop.antiguedad} años`,
      etiqueta: 'Antigüedad',
    });
  }

  if (prop.mascotas) {
    specs.push({ icono: '🐾', valor: 'Sí', etiqueta: 'Mascotas' });
  }

  contenedor.innerHTML = specs.map(spec => `
    <div class="chip_spec" role="listitem">
      <span class="icono_chip_spec" aria-hidden="true">${spec.icono}</span>
      <span class="valor_chip_spec">${spec.valor}</span>
      <span class="etiqueta_chip_spec">${spec.etiqueta}</span>
    </div>
  `).join('');
}

/**
 * Renderiza la lista de características como chips.
 * @param {string[]} caracteristicas
 */
function renderizar_caracteristicas(caracteristicas) {
  const contenedor = obtener_elemento_detalle('lista_caracteristicas');
  if (!contenedor) return;

  if (!caracteristicas.length) {
    contenedor.innerHTML = '<p class="texto_vacio">Sin características registradas.</p>';
    return;
  }

  contenedor.innerHTML = caracteristicas.map(c => `
    <div class="chip_caracteristica" role="listitem">
      <span class="icono_check_caracteristica" aria-hidden="true">✓</span>
      ${c}
    </div>
  `).join('');
}

/**
 * Renderiza la grilla de galería en la columna izquierda.
 * @param {string[]} imagenes
 */
function renderizar_grilla_galeria(imagenes) {
  const contenedor = obtener_elemento_detalle('grilla_galeria');
  if (!contenedor) return;

  contenedor.innerHTML = imagenes.map((img, i) => `
    <div
      class="celda_galeria"
      role="listitem"
      tabindex="0"
      aria-label="Foto ${i + 1} de la propiedad"
      onclick="abrir_galeria(${i})"
      onkeydown="if(event.key==='Enter') abrir_galeria(${i})"
    >
      <img
        src="${img}"
        alt="Foto ${i + 1}"
        loading="lazy"
      />
      <div class="overlay_galeria" aria-hidden="true">
        <svg class="icono_expandir_galeria" width="22" height="22" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
          <polyline points="15 3 21 3 21 9"/><polyline points="9 21 3 21 3 15"/>
          <line x1="21" y1="3" x2="14" y2="10"/><line x1="3" y1="21" x2="10" y2="14"/>
        </svg>
      </div>
    </div>
  `).join('');
}

/**
 * Renderiza la tarjeta de contacto con los datos del asesor.
 * @param {Object} prop
 */
function renderizar_contacto(prop) {
  const precio = obtener_elemento_detalle('precio_contacto');
  if (precio) {
    precio.textContent = prop.precio_etiqueta || `$${(prop.precio || 0).toLocaleString('es-CO')}`;
  }

  const nombre_asesor = prop.nombreVendedor || prop.asesor?.nombre || 'Asesor comercial';
  const telefono_asesor = prop.telefonoVendedor || prop.asesor?.telefono || '';
  const correo_asesor = prop.correoVendedor || prop.asesor?.email || '';
  const inicial = prop.asesor?.inicial || nombre_asesor.charAt(0).toUpperCase();

  const avatar = obtener_elemento_detalle('avatar_asesor');
  if (avatar) avatar.textContent = inicial;

  const nombre = obtener_elemento_detalle('nombre_asesor');
  if (nombre) nombre.textContent = nombre_asesor;

  const enlace_llamar = obtener_elemento_detalle('enlace_llamar');
  if (enlace_llamar) enlace_llamar.href = `tel:${telefono_asesor}`;

  const enlace_email = obtener_elemento_detalle('enlace_email');
  if (enlace_email) enlace_email.href = `mailto:${correo_asesor}`;

  const form_contacto = obtener_elemento_detalle('formulario_contacto');
  if (form_contacto) {
    form_contacto.dataset.idVendedor = prop.idVendedor || '';
    form_contacto.dataset.idInmueble = prop.idInmueble || prop.id || '';
  }

  /* Subtexto del estado de éxito */
  const subtexto = obtener_elemento_detalle('subtexto_exito_contacto');
  if (subtexto) subtexto.textContent = `${nombre_asesor} te contactará pronto.`;
}


/* ════════════════════════════════════════════════════════════
   GALERÍA MODAL
════════════════════════════════════════════════════════════ */

/**
 * Abre el modal de galería en el índice indicado.
 * @param {number} indice
 */
function abrir_galeria(indice) {
  const prop = estado_detalle.propiedad_actual;
  if (!prop) return;

  estado_detalle.indice_galeria = indice;
  actualizar_imagen_galeria();

  const modal = obtener_elemento_detalle('modal_galeria');
  if (modal) {
    modal.classList.remove('oculto');
    document.body.style.overflow = 'hidden';
    const boton_cerrar = obtener_elemento_detalle('boton_cerrar_galeria');
    if (boton_cerrar) boton_cerrar.focus();
  }
}

/**
 * Cierra el modal de galería.
 */
function cerrar_galeria() {
  const modal = obtener_elemento_detalle('modal_galeria');
  if (modal) {
    modal.classList.add('oculto');
    document.body.style.overflow = '';
  }
}

/**
 * Navega entre fotos de la galería.
 * @param {number} direccion - +1 siguiente, -1 anterior
 */
function navegar_galeria(direccion) {
  const prop = estado_detalle.propiedad_actual;
  if (!prop) return;

  const imagenes = prop.imagenes?.length ? prop.imagenes : [prop.imagen_hero];
  const total = imagenes.length;

  estado_detalle.indice_galeria = (estado_detalle.indice_galeria + direccion + total) % total;
  actualizar_imagen_galeria();
}

/**
 * Actualiza la imagen activa y el contador en el modal.
 */
function actualizar_imagen_galeria() {
  const prop = estado_detalle.propiedad_actual;
  if (!prop) return;

  const imagenes = prop.imagenes?.length ? prop.imagenes : [prop.imagen_hero];
  const img = obtener_elemento_detalle('imagen_galeria_activa');
  const contador = obtener_elemento_detalle('contador_galeria');

  if (img) {
    img.classList.add('cambiando');
    img.src = imagenes[estado_detalle.indice_galeria];
    img.alt = `Foto ${estado_detalle.indice_galeria + 1} de ${prop.titulo}`;
    img.addEventListener('animationend', () => img.classList.remove('cambiando'), { once: true });
  }

  if (contador) {
    contador.textContent = `${estado_detalle.indice_galeria + 1} / ${imagenes.length}`;
  }
}


/* ════════════════════════════════════════════════════════════
   FAVORITO Y COMPARTIR
════════════════════════════════════════════════════════════ */

/**
 * Alterna el estado de favorito.
 */
function alternar_favorito_detalle() {
  estado_detalle.es_favorito = !estado_detalle.es_favorito;

  const boton = obtener_elemento_detalle('boton_favorito_hero');
  if (boton) {
    boton.setAttribute('aria-pressed', String(estado_detalle.es_favorito));
    boton.classList.toggle('favorito_activo', estado_detalle.es_favorito);
  }

  /* Guardar/eliminar de localStorage */
  const prop = estado_detalle.propiedad_actual;
  if (prop) {
    const id = prop.idInmueble || prop.id;
    let favoritos = JSON.parse(localStorage.getItem('favoritos') || '[]');

    if (estado_detalle.es_favorito) {
      if (!favoritos.includes(id)) favoritos.push(id);
    } else {
      favoritos = favoritos.filter(fId => fId !== id);
    }

    localStorage.setItem('favoritos', JSON.stringify(favoritos));
  }

  const mensaje = estado_detalle.es_favorito
    ? '❤️ Guardado en favoritos'
    : 'Removido de favoritos';
  mostrar_notificacion_detalle(mensaje, estado_detalle.es_favorito ? 'exito' : 'info');
}

/**
 * Restaura el estado visual de favoritos desde localStorage.
 * @param {number|string} id
 */
function restaurar_estado_favorito(id) {
  const favoritos_guardados = localStorage.getItem('favoritos');
  if (favoritos_guardados) {
    const favoritos = JSON.parse(favoritos_guardados);
    if (favoritos.includes(id)) {
      estado_detalle.es_favorito = true;
      const boton = obtener_elemento_detalle('boton_favorito_hero');
      if (boton) {
        boton.setAttribute('aria-pressed', 'true');
        boton.classList.add('favorito_activo');
      }
    }
  }
}

/**
 * Simula compartir la propiedad (usa la API Web Share si está disponible).
 */
function compartir_propiedad() {
  const prop = estado_detalle.propiedad_actual;
  if (!prop) return;

  const precio = prop.precio_etiqueta || `$${(prop.precio || 0).toLocaleString('es-CO')}`;

  if (navigator.share) {
    navigator.share({
      title: prop.titulo,
      text: `Mira esta propiedad en Rever Inmobiliaria: ${prop.titulo} — ${precio}`,
      url: window.location.href,
    }).catch(() => {});
  } else {
    navigator.clipboard.writeText(window.location.href).then(() => {
      mostrar_notificacion_detalle('Enlace copiado al portapapeles', 'info');
    }).catch(() => {
      mostrar_notificacion_detalle('No se pudo compartir la propiedad', 'error');
    });
  }
}


/* ════════════════════════════════════════════════════════════
   FORMULARIO DE CONTACTO
════════════════════════════════════════════════════════════ */

/**
 * Muestra el formulario de contacto o un aviso para iniciar sesión,
 * según si hay un usuario logueado.
 */
function actualizar_estado_formulario_contacto() {
  const usuario = obtener_usuario_actual();
  const formulario = document.getElementById('formulario_contacto');
  const prompt_login = document.getElementById('prompt_login_contacto');
  const estado_exito = document.getElementById('estado_exito_contacto');

  if (usuario) {
    formulario.classList.remove('oculto');
    prompt_login.classList.add('oculto');
  } else {
    formulario.classList.add('oculto');
    estado_exito.classList.add('oculto');
    prompt_login.classList.remove('oculto');

    // Guarda a dónde volver después de iniciar sesión/registrarse
    const url_actual = window.location.pathname + window.location.search;
    document.getElementById('enlace_login_contacto').href = `login.html?volver=${encodeURIComponent(url_actual)}`;
    document.getElementById('enlace_registro_contacto').href = `registro.html?volver=${encodeURIComponent(url_actual)}`;
  }
}

/**
 * Maneja el envío del formulario de contacto.
 * @param {Event} evento
 */
async function manejar_envio_contacto(evento) {
  evento.preventDefault();

  const usuario = obtener_usuario_actual();
  const token = localStorage.getItem('token');

  if (!usuario) {
    mostrar_notificacion('Debes iniciar sesión para contactar al vendedor', 'error');
    return;
  }

  const mensaje = document.getElementById('input_mensaje_contacto').value.trim();
  const error_mensaje = document.getElementById('error_mensaje');

  if (!mensaje) {
    error_mensaje.classList.remove('oculto');
    return;
  }
  error_mensaje.classList.add('oculto');

  const formulario = document.getElementById('formulario_contacto');
  const idVendedor = parseInt(formulario.dataset.idVendedor, 10);
  const idInmueble = parseInt(formulario.dataset.idInmueble, 10);

  const boton = document.getElementById('boton_enviar_contacto');
  boton.disabled = true;
  boton.textContent = 'Enviando...';

  try {
    const respuesta = await fetch(`${API_URL}/Contacto`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({
        idVendedor: idVendedor,
        idInmueble: idInmueble,
        mensaje: mensaje,
        fecha: new Date().toISOString(),
      }),
    });

    if (!respuesta.ok) {
      const texto = await respuesta.text();
      throw new Error(texto || 'No se pudo enviar el mensaje');
    }

    document.getElementById('formulario_contacto').classList.add('oculto');
    document.getElementById('estado_exito_contacto').classList.remove('oculto');
    document.getElementById('subtexto_exito_contacto').textContent =
      'Tu mensaje fue enviado al asesor. Te responderá pronto.';

  } catch (error) {
    console.error(error);
    mostrar_notificacion(error.message, 'error');
  } finally {
    boton.disabled = false;
    boton.textContent = 'Enviar mensaje';
  }
}

function restablecer_formulario() {
  document.getElementById('formulario_contacto').reset();
  document.getElementById('formulario_contacto').classList.remove('oculto');
  document.getElementById('estado_exito_contacto').classList.add('oculto');
}


/* ════════════════════════════════════════════════════════════
   CALENDARIO DE VISITA
════════════════════════════════════════════════════════════ */

/**
 * Inicializa el calendario de visitas
 */
function inicializar_calendario_visita() {
  renderizar_calendario();
}

/**
 * Renderiza el calendario del mes actual
 */
function renderizar_calendario() {
  const mes = estado_detalle.calendario.mes_actual;
  const grid_dias = document.getElementById('grid_dias_calendario');
  const texto_mes = document.getElementById('texto_mes_actual');

  if (!grid_dias || !texto_mes) return;

  // Nombre del mes y año
  const nombres_meses = [
    'Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio',
    'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'
  ];
  texto_mes.textContent = `${nombres_meses[mes.getMonth()]} ${mes.getFullYear()}`;

  // Primer día del mes y total de días
  const primer_dia = new Date(mes.getFullYear(), mes.getMonth(), 1);
  const ultimo_dia = new Date(mes.getFullYear(), mes.getMonth() + 1, 0);
  const total_dias = ultimo_dia.getDate();
  const dia_semana_inicio = primer_dia.getDay();

  // Fecha actual para comparación
  const hoy = new Date();
  hoy.setHours(0, 0, 0, 0);

  let html = '';

  // Días vacíos antes del primer día del mes
  for (let i = 0; i < dia_semana_inicio; i++) {
    html += '<div class="dia_calendario dia_vacio"></div>';
  }

  // Días del mes
  for (let dia = 1; dia <= total_dias; dia++) {
    const fecha = new Date(mes.getFullYear(), mes.getMonth(), dia);
    fecha.setHours(0, 0, 0, 0);

    const es_hoy = fecha.getTime() === hoy.getTime();
    const es_pasado = fecha < hoy;
    const es_seleccionado = estado_detalle.calendario.dia_seleccionado &&
                           fecha.getTime() === estado_detalle.calendario.dia_seleccionado.getTime();

    let clases = 'dia_calendario';
    if (es_hoy) clases += ' dia_hoy';
    if (es_pasado) clases += ' dia_inhabilitado';
    if (es_seleccionado) clases += ' dia_seleccionado';

    html += `<div class="${clases}" ${!es_pasado ? `onclick="seleccionar_dia_calendario(${dia})"` : ''}>${dia}</div>`;
  }

  grid_dias.innerHTML = html;
}

/**
 * Navega al mes anterior o siguiente
 * @param {number} direccion - -1 para anterior, 1 para siguiente
 */
function navegar_mes_calendario(direccion) {
  const nuevo_mes = new Date(estado_detalle.calendario.mes_actual);
  nuevo_mes.setMonth(nuevo_mes.getMonth() + direccion);

  // No permitir navegar a meses pasados
  const hoy = new Date();
  hoy.setHours(0, 0, 0, 0);
  const inicio_nuevo_mes = new Date(nuevo_mes.getFullYear(), nuevo_mes.getMonth(), 1);
  inicio_nuevo_mes.setHours(0, 0, 0, 0);

  if (inicio_nuevo_mes < hoy) return;

  estado_detalle.calendario.mes_actual = nuevo_mes;
  renderizar_calendario();
}

/**
 * Selecciona un día del calendario
 * @param {number} dia
 */
function seleccionar_dia_calendario(dia) {
  const fecha = new Date(
    estado_detalle.calendario.mes_actual.getFullYear(),
    estado_detalle.calendario.mes_actual.getMonth(),
    dia
  );
  fecha.setHours(0, 0, 0, 0);

  estado_detalle.calendario.dia_seleccionado = fecha;
  estado_detalle.calendario.hora_seleccionada = null;

  renderizar_calendario();
  renderizar_horas_disponibles(fecha);
}

/**
 * Renderiza las horas disponibles para una fecha
 * @param {Date} fecha
 */
function renderizar_horas_disponibles(fecha) {
  const contenedor_horas = document.getElementById('contenedor_horas');
  const grid_horas = document.getElementById('grid_horas');
  const fecha_texto = document.getElementById('fecha_seleccionada_texto');
  const resumen = document.getElementById('resumen_seleccion');

  if (!contenedor_horas || !grid_horas) return;

  // Formatear fecha
  const opciones = { weekday: 'long', day: 'numeric', month: 'long' };
  const fecha_formateada = fecha.toLocaleDateString('es-ES', opciones);
  fecha_texto.textContent = fecha_formateada.charAt(0).toUpperCase() + fecha_formateada.slice(1);

  // Horas disponibles (9:00 - 18:00)
  const horas = [
    '9:00', '10:00', '11:00', '12:00',
    '14:00', '15:00', '16:00', '17:00', '18:00'
  ];

  // Simular algunas horas ocupadas aleatoriamente
  const hoy = new Date();
  hoy.setHours(0, 0, 0, 0);
  const es_hoy = fecha.getTime() === hoy.getTime();
  const hora_actual = hoy.getHours();

  grid_horas.innerHTML = horas.map(hora => {
    const hora_num = parseInt(hora.split(':')[0], 10);
    const es_hora_pasada = es_hoy && hora_num <= hora_actual;
    const es_ocupada = Math.random() < 0.3; // 30% de probabilidad de estar ocupada

    let clases = 'hora_disponible';
    if (es_hora_pasada || es_ocupada) clases += ' hora_inhabilitada';

    return `<div class="${clases}" ${!es_hora_pasada && !es_ocupada ? `onclick="seleccionar_hora('${hora}')"` : ''}>${hora}</div>`;
  }).join('');

  contenedor_horas.classList.remove('oculto');
  resumen.classList.add('oculto');
}

/**
 * Selecciona una hora
 * @param {string} hora
 */
function seleccionar_hora(hora) {
  estado_detalle.calendario.hora_seleccionada = hora;

  // Actualizar UI
  document.querySelectorAll('.hora_disponible').forEach(el => {
    el.classList.remove('hora_seleccionada');
    if (el.textContent === hora) {
      el.classList.add('hora_seleccionada');
    }
  });

  // Mostrar resumen
  const fecha = estado_detalle.calendario.dia_seleccionado;
  const opciones = { weekday: 'long', day: 'numeric', month: 'long' };
  const fecha_formateada = fecha.toLocaleDateString('es-ES', opciones);
  const fecha_hora = `${fecha_formateada.charAt(0).toUpperCase() + fecha_formateada.slice(1)} a las ${hora}`;

  document.getElementById('fecha_hora_resumen').textContent = fecha_hora;
  document.getElementById('contenedor_horas').classList.add('oculto');
  document.getElementById('resumen_seleccion').classList.remove('oculto');
}

/**
 * Reinicia la selección del calendario
 */
function reiniciar_seleccion_calendario() {
  estado_detalle.calendario.dia_seleccionado = null;
  estado_detalle.calendario.hora_seleccionada = null;

  document.getElementById('contenedor_horas').classList.add('oculto');
  document.getElementById('resumen_seleccion').classList.add('oculto');
  renderizar_calendario();
}


/* ════════════════════════════════════════════════════════════
   INICIALIZACIÓN & EVENT LISTENERS
════════════════════════════════════════════════════════════ */

/**
 * Punto de entrada principal de la pantalla de detalle.
 */
function inicializar_detalle() {
  /* Cargar datos desde la API */
  cargar_detalle_inmueble();

  /* Inicializar calendario de visitas */
  inicializar_calendario_visita();

  /* ── Event Listeners globales ── */

  /* Cerrar galería con Escape / Navegación con Teclado */
  document.addEventListener('keydown', evento => {
    if (evento.key === 'Escape') {
      cerrar_galeria();
    }

    const modal = obtener_elemento_detalle('modal_galeria');
    if (modal && !modal.classList.contains('oculto')) {
      if (evento.key === 'ArrowLeft')  navegar_galeria(-1);
      if (evento.key === 'ArrowRight') navegar_galeria(1);
    }
  });

  /* Swipe táctil en galería */
  let inicio_touch_x = null;
  const modal_galeria = obtener_elemento_detalle('modal_galeria');

  if (modal_galeria) {
    modal_galeria.addEventListener('touchstart', evento => {
      inicio_touch_x = evento.changedTouches[0].screenX;
    }, { passive: true });

    modal_galeria.addEventListener('touchend', evento => {
      if (inicio_touch_x === null) return;
      const delta = evento.changedTouches[0].screenX - inicio_touch_x;
      if (Math.abs(delta) > 50) {
        navegar_galeria(delta < 0 ? 1 : -1);
      }
      inicio_touch_x = null;
    }, { passive: true });
  }
}

/* Ejecutar al cargar el DOM */
document.addEventListener('DOMContentLoaded', inicializar_detalle);

/**
 * Función para volver al index restaurando el estado de filtros
 */
function volver_con_estado() {
  window.location.href = 'index.html';
}
/* ═══════════════════════════════════════════════════════
   mainpublicar_inmueble.js — Rever Plataforma Inmobiliaria
   Conectado a la base de datos real vía fetch
   Ubicación: Ciudad → Localidad
   ═══════════════════════════════════════════════════════ */

const estado_publicar = {
  fotos_seleccionadas: [],
  caracteristicas_disponibles: [],
  caracteristicas_seleccionadas: new Set(),
  contadores: { habitaciones: 1, banios: 1, parqueaderos: 0, piso: 1 },
  catalogo_localidades: [],
};

/* ──────────────────────────────────────────────────────
   0. UTILIDADES
   ────────────────────────────────────────────────────── */

// Busca el ID de un objeto del catálogo. Prueba primero los nombres
// esperados y, si ninguno existe, usa la primera propiedad que empiece por "id".
function obtener_id(objeto, nombres_preferidos = []) {
  for (const nombre of nombres_preferidos) {
    if (objeto[nombre] !== undefined && objeto[nombre] !== null) return objeto[nombre];
  }
  const clave = Object.keys(objeto).find(k => k.toLowerCase().startsWith('id'));
  return clave ? objeto[clave] : undefined;
}

// Muestra los errores en una caja roja visible encima del botón de publicar
function mostrar_errores_formulario(mensajes) {
  let caja = document.getElementById('caja_errores');
  if (!caja) {
    caja = document.createElement('div');
    caja.id = 'caja_errores';
    caja.setAttribute('role', 'alert');
    caja.style.cssText =
      'background:#fdecea;border:1px solid #f5c2c0;color:#8a1f17;' +
      'padding:12px 16px;border-radius:8px;margin:16px 0;';
    document.querySelector('.barra_envio').before(caja);
  }
  caja.innerHTML =
    '<strong>No se pudo publicar:</strong>' +
    '<ul style="margin:8px 0 0 18px">' +
    mensajes.map(m => `<li>${m}</li>`).join('') +
    '</ul>';
  caja.scrollIntoView({ behavior: 'smooth', block: 'center' });
}

function limpiar_errores_formulario() {
  document.getElementById('caja_errores')?.remove();
}

/* ──────────────────────────────────────────────────────
   1. CONTADORES NUMÉRICOS
   ────────────────────────────────────────────────────── */
function incrementar_contador(campo) {
  estado_publicar.contadores[campo]++;
  actualizar_visualizacion_contador(campo);
}

function decrementar_contador(campo, minimo = 0) {
  if (estado_publicar.contadores[campo] > minimo) {
    estado_publicar.contadores[campo]--;
    actualizar_visualizacion_contador(campo);
  }
}

function actualizar_visualizacion_contador(campo) {
  const el = document.getElementById(campo);
  if (el) el.textContent = estado_publicar.contadores[campo];
}


/* ──────────────────────────────────────────────────────
   2. AMENIDADES (reales, cargadas desde la BD)
   ────────────────────────────────────────────────────── */
function alternar_amenidad(id) {
  if (estado_publicar.caracteristicas_seleccionadas.has(id)) {
    estado_publicar.caracteristicas_seleccionadas.delete(id);
  } else {
    estado_publicar.caracteristicas_seleccionadas.add(id);
  }
  renderizar_amenidades();
}

function renderizar_amenidades() {
  const contenedor = document.getElementById('grupo_amenidades');
  if (!contenedor) return;

  contenedor.innerHTML = estado_publicar.caracteristicas_disponibles.map((c) => {
    const activo = estado_publicar.caracteristicas_seleccionadas.has(c.idCaracteristica);
    return `
      <button
        type="button"
        class="chip_amenidad ${activo ? 'chip_amenidad--seleccionado' : ''}"
        onclick="alternar_amenidad(${c.idCaracteristica})"
        aria-pressed="${activo}"
      >
        ${activo ? '<span class="chip_amenidad__icono_check">✓</span>' : ''}
        ${c.nombre}
      </button>
    `;
  }).join('');
}


/* ──────────────────────────────────────────────────────
   3. CASCADA CIUDAD → LOCALIDAD
   Si la ciudad tiene una sola localidad, se selecciona sola
   y el campo se oculta. Si tiene varias, el usuario elige.
   ────────────────────────────────────────────────────── */
function manejar_cambio_ciudad() {
  const select_ciudad = document.getElementById('ciudad');
  const contenedor_localidad = document.getElementById('contenedor_localidad');
  const select_localidad = document.getElementById('localidad');

  const idCiudad = parseInt(select_ciudad.value, 10);

  select_localidad.innerHTML = '<option value="" disabled selected>Selecciona localidad</option>';

  if (!idCiudad) {
    contenedor_localidad.classList.add('oculto');
    return;
  }

  const localidades_de_la_ciudad = estado_publicar.catalogo_localidades.filter(
    l => l.idCiudad === idCiudad
  );
  localidades_de_la_ciudad.forEach(l =>
    select_localidad.add(new Option(l.nombre, l.idLocalidad))
  );

  if (localidades_de_la_ciudad.length === 1) {
    select_localidad.value = localidades_de_la_ciudad[0].idLocalidad;
    contenedor_localidad.classList.add('oculto');
  } else {
    contenedor_localidad.classList.remove('oculto');
  }
}


/* ──────────────────────────────────────────────────────
   4. MAPA
   ────────────────────────────────────────────────────── */
function abrir_mapa() {
  const select_ciudad = document.getElementById('ciudad');
  const ciudad = select_ciudad?.options[select_ciudad.selectedIndex]?.text || '';
  const direccion = document.getElementById('direccion')?.value || '';
  const consulta = encodeURIComponent(`${ciudad} ${direccion}`.trim() || 'Colombia');
  window.open(`https://www.google.com/maps/search/${consulta}`, '_blank', 'noopener');
}


/* ──────────────────────────────────────────────────────
   5. FOTOGRAFÍAS (por URL)
   ────────────────────────────────────────────────────── */

function agregar_foto_por_url() {
  const input = document.getElementById('input_url_foto');
  const url = input.value.trim();
  if (!url) return;

  estado_publicar.fotos_seleccionadas.push(url);
  input.value = '';
  renderizar_galeria_fotos();
}

function eliminar_foto(indice) {
  estado_publicar.fotos_seleccionadas.splice(indice, 1);
  renderizar_galeria_fotos();
}

function renderizar_galeria_fotos() {
  const galeria = document.getElementById('galeria_fotos');
  if (!galeria) return;

  galeria.innerHTML = estado_publicar.fotos_seleccionadas.map((url, idx) => `
    <li class="galeria_fotos__miniatura">
      <img src="${url}" alt="" class="galeria_fotos__imagen" />
      ${idx === 0 ? '<span class="galeria_fotos__etiqueta_principal">Portada</span>' : ''}
      <div class="galeria_fotos__superposicion">
        <button type="button" class="galeria_fotos__boton_eliminar" onclick="eliminar_foto(${idx})" aria-label="Eliminar foto">✕</button>
      </div>
    </li>
  `).join('');

  const contador = document.getElementById('contador_fotos');
  if (contador) {
    contador.textContent = `${estado_publicar.fotos_seleccionadas.length} fotos agregadas. La primera será la imagen principal (portada).`;
  }
}


/* ──────────────────────────────────────────────────────
   6. DESCRIPCIÓN
   ────────────────────────────────────────────────────── */
function actualizar_contador_descripcion() {
  const textarea = document.getElementById('descripcion');
  const contador = document.getElementById('contador_descripcion');
  if (textarea && contador) {
    contador.textContent = `${textarea.value.length} / 800 caracteres`;
  }
}


/* ──────────────────────────────────────────────────────
   7. CARGA DE CATÁLOGOS
   ────────────────────────────────────────────────────── */
async function cargar_catalogos() {
  try {
    const [tipos, modos, ciudades, caracteristicas, localidades] = await Promise.all([
      fetch(`${API_URL}/TipoInmueble`).then(r => r.json()),
      fetch(`${API_URL}/ModoTransaccion`).then(r => r.json()),
      fetch(`${API_URL}/Ciudad`).then(r => r.json()),
      fetch(`${API_URL}/Caracteristica`).then(r => r.json()),
      fetch(`${API_URL}/Localidad`).then(r => r.json()),
    ]);

    // Tipo y modo: se detecta el nombre real del ID (idTipo, idTipoInmueble, id, etc.)
    const select_tipo = document.getElementById('tipo_inmueble');
    tipos.forEach(t =>
      select_tipo.add(new Option(t.nombre, obtener_id(t, ['idTipo', 'idTipoInmueble'])))
    );

    const select_modo = document.getElementById('tipo_operacion');
    modos.forEach(m =>
      select_modo.add(new Option(m.nombre, obtener_id(m, ['idModo', 'idModoTransaccion'])))
    );

    const select_ciudad = document.getElementById('ciudad');
    ciudades.forEach(c => select_ciudad.add(new Option(c.nombre, c.idCiudad)));

    estado_publicar.caracteristicas_disponibles = caracteristicas;
    estado_publicar.catalogo_localidades = localidades;
    renderizar_amenidades();
  } catch (error) {
    console.error(error);
    mostrar_notificacion('No se pudieron cargar las opciones del formulario', 'error');
  }

  const usuario = obtener_usuario_actual();
  const texto_usuario = document.getElementById('texto_usuario_sesion');
  if (usuario && texto_usuario) {
    texto_usuario.textContent = `${usuario.nombre} (${usuario.correo})`;
  }
}


/* ──────────────────────────────────────────────────────
   8. ENVÍO DEL FORMULARIO (COMPLETO)
   ────────────────────────────────────────────────────── */
async function manejar_envio_formulario(evento) {
  evento.preventDefault();
  limpiar_errores_formulario();

  const usuario = obtener_usuario_actual();
  const token = localStorage.getItem('token');

  if (!usuario) {
    mostrar_errores_formulario(['Debes iniciar sesión para publicar.']);
    return;
  }

  // Sanitización de valores numéricos para evitar NaN
  const obtener_numero = (id) => {
    const el = document.getElementById(id);
    if (!el || !el.value) return 0;
    const num = parseInt(el.value.replace(/\D/g, ''), 10);
    return isNaN(num) ? 0 : num;
  };

  const obtener_entero_select = (id) => {
    const num = parseInt(document.getElementById(id)?.value || '0', 10);
    return isNaN(num) ? 0 : num;
  };

  // Procesamiento de amenidades / características
  const caracteristicas_finales = new Set(estado_publicar.caracteristicas_seleccionadas);
  if (estado_publicar.contadores.parqueaderos > 0) {
    const parqueadero = estado_publicar.caracteristicas_disponibles?.find(c => c.nombre === 'Parqueadero');
    if (parqueadero) caracteristicas_finales.add(parqueadero.idCaracteristica);
  }

  // PAYLOAD (los nombres coinciden con CrearInmuebleDto)
  const payload = {
    titulo: document.getElementById('nombre_inmueble')?.value?.trim() || '',
    descripcion: document.getElementById('descripcion')?.value?.trim() || '',
    precio: obtener_numero('precio'),
    idTipo: obtener_entero_select('tipo_inmueble'),
    idModo: obtener_entero_select('tipo_operacion'),
    direccion: document.getElementById('direccion')?.value?.trim() || '',
    idLocalidad: obtener_entero_select('localidad'),

    habitaciones: estado_publicar.contadores.habitaciones || 0,
    banos: estado_publicar.contadores.banios || 0,
    piso: estado_publicar.contadores.piso || 1,
    metrosCuadrados: obtener_numero('area'),
    estrato: obtener_entero_select('estrato'),

    latitud: 0,
    longitud: 0,

    caracteristicasIds: Array.from(caracteristicas_finales),
    imagenesUrls: estado_publicar.fotos_seleccionadas || []
  };

  try {
    const respuesta = await fetch(`${API_URL}/Inmueble/completo`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        ...(token ? { Authorization: `Bearer ${token}` } : {}),
      },
      body: JSON.stringify(payload),
    });

    if (!respuesta.ok) {
      const texto = await respuesta.text();
      let mensajes = [];
      try {
        const json = JSON.parse(texto);
        mensajes = json.errors
          ? Object.values(json.errors).flat()
          : [json.title || texto];
      } catch {
        mensajes = [texto || 'No se pudo publicar el inmueble.'];
      }
      mostrar_errores_formulario(mensajes);
      return;
    }

    const resultado = await respuesta.json();
    const el_radicado = document.getElementById('numero_radicado');
    if (el_radicado) el_radicado.textContent = `RV-${resultado.idInmueble}`;

    mostrar_notificacion('¡Inmueble publicado con éxito!', 'exito');

    const modal = document.getElementById('modal_confirmacion');
    if (modal) modal.classList.remove('modal_confirmacion--oculto');

  } catch (error) {
    console.error('Error al publicar:', error);
    mostrar_errores_formulario(['No se pudo conectar con el servidor. Revisa que la API esté corriendo.']);
  }
}


/* ──────────────────────────────────────────────────────
   9. INICIALIZACIÓN
   ────────────────────────────────────────────────────── */
function inicializar_formulario_publicacion() {
  const formulario = document.getElementById('formulario_publicacion');
  if (formulario) {
    // Único punto de envío (el HTML no tiene onsubmit)
    formulario.addEventListener('submit', manejar_envio_formulario);
  }

  const select_ciudad = document.getElementById('ciudad');
  if (select_ciudad) {
    select_ciudad.addEventListener('change', manejar_cambio_ciudad);
  }

  const textarea_desc = document.getElementById('descripcion');
  if (textarea_desc) {
    textarea_desc.addEventListener('input', actualizar_contador_descripcion);
  }

  cargar_catalogos();
  renderizar_galeria_fotos();
  ['habitaciones', 'banios', 'parqueaderos', 'piso'].forEach(actualizar_visualizacion_contador);
}

if (document.readyState === 'loading') {
  document.addEventListener('DOMContentLoaded', inicializar);
} else {
  inicializar();
}
/* ═══════════════════════════════════════════════════════════════
   REVER INMOBILIARIA — JavaScript Principal
   Nomenclatura: Español + snake_case
═══════════════════════════════════════════════════════════════ */

'use strict';

/* ── Constantes ── */
const RETRASO_ANIMACION   = 1500;
const TIEMPO_NOTIFICACION = 3000;

/* ── Estado global de la aplicación ── */
const estado = {
  sesion_activa: false,
  nombre_usuario: null,
  email_usuario:  null,
  imagen_usuario: null,
  modo_navegacion: 'todos',
  sidebar_abierto: true,
  menu_usuario_abierto: false,
  catalogo_localidades: [],
  filtros: {
    tipos:    new Set(),
    precio_max: null,
    area_max: null,
    habitaciones: null,
    habitaciones_es_minimo: false,
    banos:    null,
    banos_es_minimo: false,
    pisos:    null,          
    pisos_es_minimo: false,    
    ciudad: null,
    localidad: null,
    estacionamientos: false,
    mascotas: false,
    caracteristicas: new Set(),
  },
  propiedades: [],
};

/* ── Base de datos de propiedades ── */
const API_URL = 'https://localhost:7015/api'; // Ajusta al puerto de tu Web API

async function cargar_propiedades() {
  mostrar_esqueleto_propiedades(); 

  try {
    const respuesta = await fetch(`${API_URL}/inmueble`);
    if (!respuesta.ok) throw new Error('No se pudieron cargar las propiedades');

    const datos = await respuesta.json();

    estado.propiedades = datos.map(item => {
      const caracts = item.caracteristicas || [];
      return {
        id: item.idInmueble,
        titulo: item.titulo,
        precio_etiqueta: `$${item.precio ? item.precio.toLocaleString('es-CO') : 0}`,
        precio: item.precio,
        ubicacion: item.ubicacion,
        hab: item.habitaciones,
        banos: item.banos,
        pisos: item.pisos,
        parqueaderos: item.parqueaderos ?? item.estacionamientos ?? 0,
        area: item.metrosCuadrados,
        imagen: item.imagenUrl,
        badge: null,
        modo: item.modo === 'venta' ? 'compra' : 'arriendo',
        tipo: item.tipo,
        ciudad: item.ciudad,
        localidad: item.localidad,
        caracteristicas: caracts,
      };
    });

    renderizar_propiedades();
  } catch (error) {
    console.error(error);
    mostrar_notificacion('No se pudieron cargar las propiedades', 'error');
  }
}

async function cargar_filtro_tipo_inmueble() {
  const contenedor = obtener_elemento('lista_checkboxes_tipos');
  if (!contenedor) return;

  try {
    const tipos = await fetch(`${API_URL}/TipoInmueble`).then(r => r.json());

    contenedor.innerHTML = tipos.map(t => `
      <label class="etiqueta_checkbox">
        <input 
          type="checkbox" 
          class="checkbox_tipo" 
          value="${t.nombre}" 
          onchange="manejar_checkbox_tipo(this, '${t.nombre}')" 
        />
        <span class="caja_checkbox" aria-hidden="true"></span>
        ${t.nombre}
      </label>
    `).join('');
  } catch (error) {
    console.error('Error al cargar tipos de inmueble:', error);
  }
}

async function cargar_filtro_ubicacion() {
  const select_ciudad = obtener_elemento('filtro_ciudad');
  if (!select_ciudad) return;

  try {
    const [ciudades, localidades] = await Promise.all([
      fetch(`${API_URL}/Ciudad`).then(r => r.json()),
      fetch(`${API_URL}/Localidad`).then(r => r.json()),
    ]);

    estado.catalogo_localidades = localidades;

    ciudades.forEach(c => select_ciudad.add(new Option(c.nombre, c.idCiudad)));
  } catch (error) {
    console.error('Error cargando ubicación:', error);
  }
}

function manejar_cambio_filtro_ciudad() {
  const select_ciudad = obtener_elemento('filtro_ciudad');
  const contenedor_localidad = obtener_elemento('contenedor_filtro_localidad');
  const select_localidad = obtener_elemento('filtro_localidad');
  
  if (!select_ciudad) return;

  const idCiudad = parseInt(select_ciudad.value, 10);
  const nombre_ciudad = select_ciudad.value ? select_ciudad.options[select_ciudad.selectedIndex].text : null;

  estado.filtros.ciudad = nombre_ciudad;
  estado.filtros.localidad = null;

  select_localidad.innerHTML = '<option value="">Todas las localidades</option>';

  if (!select_ciudad.value) {
    contenedor_localidad.classList.add('oculto');
    actualizar_estado_botones_filtros();
    renderizar_propiedades();
    return;
  }

  // Filtrar localidades que pertenecen a la ciudad seleccionada
  const localidades_de_la_ciudad = estado.catalogo_localidades.filter(l => l.idCiudad === idCiudad);

  if (localidades_de_la_ciudad.length > 0) {
    contenedor_localidad.classList.remove('oculto');
    localidades_de_la_ciudad.forEach(loc => {
      select_localidad.add(new Option(loc.nombre, loc.idLocalidad));
    });
  } else {
    contenedor_localidad.classList.add('oculto');
  }

  actualizar_estado_botones_filtros();
  renderizar_propiedades();
}

function manejar_cambio_filtro_localidad() {
  const select_localidad = obtener_elemento('filtro_localidad');

  estado.filtros.localidad = select_localidad.value ? select_localidad.options[select_localidad.selectedIndex].text : null;

  actualizar_estado_botones_filtros();
  renderizar_propiedades();
}

async function cargar_caracteristicas_filtro() {
  const contenedor = obtener_elemento('lista_checkboxes_caracteristicas');
  if (!contenedor) return;

  try {
    const caracteristicas = await fetch(`${API_URL}/Caracteristica`).then(r => r.json());

    contenedor.innerHTML = caracteristicas.map(c => `
      <label class="etiqueta_checkbox">
        <input type="checkbox" class="checkbox_caracteristica" value="${c.nombre}" onchange="manejar_checkbox_caracteristica(this, '${c.nombre}')" />
        <span class="caja_checkbox" aria-hidden="true"></span>
        ${c.nombre}
      </label>
    `).join('');
  } catch (error) {
    console.error('Error al cargar características:', error);
  }
}

/* ── Menú desplegable de características ── */

function alternar_desplegable_caracteristicas() {
  const boton = obtener_elemento('boton_desplegable_caracteristicas');
  const panel = obtener_elemento('panel_caracteristicas');
  if (!boton || !panel) return;

  const abrir = panel.hidden;
  panel.hidden = !abrir;
  boton.setAttribute('aria-expanded', abrir);
  boton.classList.toggle('abierto', abrir);

  if (abrir) {
    const buscador = obtener_elemento('buscador_caracteristicas');
    if (buscador) buscador.focus();
  }
}

function actualizar_contador_caracteristicas() {
  const contador = obtener_elemento('contador_caracteristicas');
  if (!contador) return;

  const total = estado.filtros.caracteristicas.size;
  contador.textContent = total;
  contador.hidden = total === 0;
}

function normalizar_texto(texto) {
  return (texto || '').toLowerCase().normalize('NFD').replace(/[\u0300-\u036f]/g, '').trim();
}

function filtrar_lista_caracteristicas(texto) {
  const consulta = normalizar_texto(texto);
  const etiquetas = document.querySelectorAll('#lista_checkboxes_caracteristicas .etiqueta_checkbox');
  let visibles = 0;

  etiquetas.forEach(etiqueta => {
    const coincide = normalizar_texto(etiqueta.textContent).includes(consulta);
    etiqueta.style.display = coincide ? '' : 'none';
    if (coincide) visibles++;
  });

  const aviso = obtener_elemento('sin_resultados_caracteristicas');
  if (aviso) aviso.hidden = visibles > 0 || etiquetas.length === 0;
}

function manejar_checkbox_caracteristica(checkbox, nombre) {
  if (checkbox.checked) {
    estado.filtros.caracteristicas.add(nombre);
  } else {
    estado.filtros.caracteristicas.delete(nombre);
  }
  actualizar_estado_botones_filtros();
  renderizar_propiedades();
}

/**
 * Decodifica el payload de un JWT.
 */
function decodificar_token(token) {
  try {
    const payload = token.split('.')[1];
    const decodificado = atob(payload.replace(/-/g, '+').replace(/_/g, '/'));
    return JSON.parse(decodificado);
  } catch {
    return null;
  }
}

/**
 * Obtiene los datos del usuario en sesión desde el JWT.
 */
function obtener_usuario_actual() {
  const token = localStorage.getItem('token');
  if (!token) return null;

  const payload = decodificar_token(token);
  if (!payload) return null;

  return {
    idUsuario: parseInt(payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'], 10),
    correo: payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
    idRol: parseInt(payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'], 10),
    nombre: payload['nombre'],
  };
}

/* ════════════════════════════════════════════════════════════
   UTILIDADES
════════════════════════════════════════════════════════════ */

function obtener_elemento(id) {
  return document.getElementById(id);
}

function formatear_precio(valor) {
  if (valor >= 100) return '$1.000M+';
  const precio = Math.round(100 + valor * 9);
  if (precio >= 1000) return '$' + (precio / 1000).toFixed(precio % 1000 === 0 ? 0 : 1) + 'B';
  return '$' + precio + 'M';
}

function toggle_carga_boton(boton, cargando, texto_normal) {
  if (cargando) {
    boton.disabled = true;
    boton.innerHTML = '<span style="display:flex;align-items:center;gap:.5rem;justify-content:center">' +
      '<svg width="16" height="16" viewBox="0 0 16 16" fill="none" style="animation:girar_carga .8s linear infinite">' +
      '<circle cx="8" cy="8" r="6" stroke="rgba(255,255,255,.3)" stroke-width="2"/>' +
      '<path d="M14 8a6 6 0 0 0-6-6" stroke="white" stroke-width="2" stroke-linecap="round"/></svg>' +
      texto_normal + '...</span>';
  } else {
    boton.disabled = false;
    boton.textContent = texto_normal;
  }
}

function mostrar_notificacion(mensaje, tipo, usar_detalle = false) {
    // Seleccionamos la función de búsqueda adecuada según el contexto
    const obtener = usar_detalle ? obtener_elemento_detalle : obtener_elemento;
    const contenedor = obtener('contenedor_notificaciones');
    if (!contenedor) return;

    const notif = document.createElement('div');
    notif.setAttribute('role', 'alert');
    notif.setAttribute('aria-live', 'polite');

    const colores = {
        exito: '#10B981',
        error: '#EF4444',
        info:  '#3B82F6',
    };

    // Usamos template literals para una lectura limpia de los estilos
    notif.style.cssText = `
        padding: .75rem 1.25rem;
        border-radius: .5rem;
        font-size: .875rem;
        font-weight: 600;
        box-shadow: 0 4px 12px rgba(0,0,0,.15);
        background: ${colores[tipo] || colores.info};
        color: #fff;
        animation: aparecer_notif .3s ease;
        max-width: 20rem;
        font-family: inherit;
    `;

    notif.textContent = mensaje;
    contenedor.appendChild(notif);

    // Definimos el tiempo usando la constante global o un respaldo de 3 segundos
    const tiempo_espera = typeof TIEMPO_NOTIFICACION !== 'undefined' ? TIEMPO_NOTIFICACION : 3000;

    setTimeout(() => {
        notif.style.animation = 'desaparecer_notif .3s ease forwards';
        setTimeout(() => notif.remove(), 300);
    }, tiempo_espera);
}
function animar_sacudir(elemento) {
  elemento.classList.add('animacion_sacudir');
  elemento.addEventListener('animationend', () => {
    elemento.classList.remove('animacion_sacudir');
  }, { once: true });
}

/* ════════════════════════════════════════════════════════════
   NAVEGACIÓN Y MODO
════════════════════════════════════════════════════════════ */

function cambiar_modo_navegacion(modo) {
  estado.modo_navegacion = modo === '' ? 'todos' : modo;

  document.querySelectorAll('[data-modo]').forEach(boton => {
    const modo_boton = boton.dataset.modo;
    boton.classList.toggle('activo', modo_boton === modo);
  });

  const titulo = obtener_elemento('titulo_seccion');
  const desc   = obtener_elemento('descripcion_seccion');
  if (titulo) {
    const titulos = {
      todos:    'Descubre tu próximo hogar',
      arriendo: 'Propiedades en arriendo',
      compra:   'Propiedades en compra',
    };
    titulo.textContent = titulos[estado.modo_navegacion] ?? titulos.todos;
  }
  if (desc) {
    const descs = {
      todos:    'Explora todas las propiedades disponibles',
      arriendo: 'Encuentra el arriendo perfecto para ti',
      compra:   'Invierte en tu propiedad ideal',
    };
    desc.textContent = descs[estado.modo_navegacion] ?? descs.todos;
  }

  if (modo === '') {
    limpiar_filtros();
  }

  cerrar_menu_movil();
  renderizar_propiedades();
}

function alternar_menu_movil() {
  const menu = obtener_elemento('menu_movil');
  if (!menu) return;
  const visible = !menu.classList.contains('oculto');
  menu.classList.toggle('oculto', visible);
}

function cerrar_menu_movil() {
  const menu = obtener_elemento('menu_movil');
  if (menu) menu.classList.add('oculto');
}

function mostrar_favoritos() {
  mostrar_notificacion('Sección de favoritos (función en construcción)', 'info');
}

function mostrar_mapa() {
  mostrar_notificacion('Mapa de propiedades (función en construcción)', 'info');
}

function alternar_menu_usuario() {
  estado.menu_usuario_abierto = !estado.menu_usuario_abierto;
  
  const menu = obtener_elemento('menu_usuario');
  const boton = document.querySelector('.boton_avatar_usuario');
  
  if (menu) {
    menu.classList.toggle('menu_usuario--oculto', !estado.menu_usuario_abierto);
    // Forzar display inline-flex cuando está abierto
    if (estado.menu_usuario_abierto) {
      menu.style.display = 'block';
    } else {
      menu.style.display = '';
    }
  }
  
  if (boton) {
    boton.setAttribute('aria-expanded', estado.menu_usuario_abierto);
  }
}

function cerrar_menu_generico(objeto_estado, propiedad, id_menu, selector_boton, clase_oculto) {
    // Si ya está cerrado, no hacemos nada (Guard Clause)
    if (!objeto_estado[propiedad]) return;

    // Cambiamos el estado
    objeto_estado[propiedad] = false;

    // Buscamos los elementos dinámicamente
    const menu = document.getElementById(id_menu);
    const boton = document.querySelector(selector_boton);

    // Aplicamos los cambios de UI y accesibilidad
    if (menu) menu.classList.add(clase_oculto);
    if (boton) boton.setAttribute("aria-expanded", "false");
}
function cerrar_menu_usuario() {
    cerrar_menu_generico(
        estado, 
        'menu_usuario_abierto', 
        'menu_usuario', 
        '.boton_avatar_usuario', 
        'menu_usuario--oculto'
    );
}

function cerrar_menu_usuario_admin() {
    cerrar_menu_generico(
        estado, 
        'menu_usuario_admin_abierto', 
        'menu_usuario_admin', 
        '.barra_lateral__avatar--clickeable', 
        'menu_usuario_admin--oculto'
    );
}

function verificar_autenticacion() {
  const usuario = obtener_usuario_actual();
  estado.sesion_activa = !!usuario;

  if (usuario) {
    estado.nombre_usuario = usuario.nombre;
    estado.email_usuario = usuario.correo;
    mostrar_opciones_autenticado();
  } else {
    mostrar_opciones_no_autenticado();
  }
}

function mostrar_opciones_autenticado() {
  const opcionesAuth = obtener_elemento('opciones_autenticado');
  const opcionesNoAuth = obtener_elemento('opciones_no_autenticado');
  
  if (opcionesAuth) opcionesAuth.classList.remove('oculto');
  if (opcionesNoAuth) opcionesNoAuth.classList.add('oculto');
}

function mostrar_opciones_no_autenticado() {
  const opcionesAuth = obtener_elemento('opciones_autenticado');
  const opcionesNoAuth = obtener_elemento('opciones_no_autenticado');
  
  if (opcionesAuth) opcionesAuth.classList.add('oculto');
  if (opcionesNoAuth) opcionesNoAuth.classList.remove('oculto');
}

function ir_a_perfil() {
  cerrar_menu_usuario();
  
  const usuario = obtener_usuario_actual();
  if (usuario && usuario.idRol === 2) {
    // Vendedor: redirigir a mis inmuebles
    window.location.href = 'mis_inmuebles.html';
  } else {
    // Comprador u otro: redirigir a perfil normal
    window.location.href = 'perfil.html';
  }
}

function ir_a_favoritos() {
  cerrar_menu_usuario();
  mostrar_notificacion('Sección de favoritos (función en construcción)', 'info');
}

function ir_a_publicaciones() {
    const usuario = obtener_usuario_actual();
    
    // Bloqueo de seguridad por si intentan forzar la función desde la consola
    if (!usuario || usuario.idRol !== 2) {
        return; 
    }

    cerrar_menu_usuario();
    window.location.href = 'mis_inmuebles.html';
}

function verificar_permisos_menu() {
    const usuario = obtener_usuario_actual();
    const btnMisInmuebles = document.getElementById('btn_mis_inmuebles');

    // Si el botón no existe en el HTML actual, evitamos errores
    if (!btnMisInmuebles) return;

    if (usuario && usuario.idRol === 2) {
        // Si es rol 2, nos aseguramos de que sea visible
        btnMisInmuebles.style.display = 'flex'; // Usamos flex para mantener el diseño de tus botones
    } else {
        // Si es cualquier otro rol (o no está logueado), se destruye visualmente del menú
        btnMisInmuebles.style.display = 'none';
    }
}

// Ejecutar automáticamente cuando cargue el script/página
document.addEventListener('DOMContentLoaded', verificar_permisos_menu);


function ir_a_login() {
  cerrar_menu_usuario();
  window.location.href = 'login.html';
}

function ir_a_registro() {
  cerrar_menu_usuario();
  window.location.href = 'registro.html';
}

function cerrar_sesion() {
  localStorage.removeItem('sesion_activa');
  localStorage.removeItem('token');
  localStorage.removeItem('usuario_actual');

  estado.sesion_activa = false;
  estado.nombre_usuario = null;
  estado.email_usuario = null;
  
  // Solo llamar funciones de UI si estamos en la página principal
  if (typeof mostrar_opciones_no_autenticado === 'function') {
    mostrar_opciones_no_autenticado();
  }
  if (typeof cerrar_menu_usuario === 'function') {
    cerrar_menu_usuario();
  }
  if (typeof mostrar_notificacion === 'function') {
    mostrar_notificacion('Sesión cerrada correctamente', 'exito');
  }
 
  // Redirigir a login después de cerrar sesión
  setTimeout(() => {
    window.location.href = 'login.html';
  }, 1000);
}

/* ════════════════════════════════════════════════════════════
   FILTROS
════════════════════════════════════════════════════════════ */

function alternar_panel_filtros() {
  estado.sidebar_abierto = !estado.sidebar_abierto;

  const sidebar = obtener_elemento('barra_filtros');
  const boton_reabrir = obtener_elemento('boton_reabrir_filtros');
  const boton_flotante = obtener_elemento('boton_toggle_filtros_flotante');

  if (sidebar) sidebar.classList.toggle('colapsada', !estado.sidebar_abierto);
  if (boton_reabrir) boton_reabrir.classList.toggle('visible', !estado.sidebar_abierto);
  if (boton_flotante) {
    boton_flotante.setAttribute('aria-expanded', estado.sidebar_abierto);
  }
}

function actualizar_estado_botones_filtros() {
  actualizar_contador_caracteristicas();

  const boton_limpiar = obtener_elemento('boton_limpiar');
  if (!boton_limpiar) return;

  const hay_filtros_activos =
    estado.filtros.tipos.size > 0 ||
    estado.filtros.precio_max !== null ||
    estado.filtros.area_max !== null ||
    estado.filtros.habitaciones !== null ||
    estado.filtros.banos !== null ||
    estado.filtros.pisos !== null ||
    estado.filtros.ciudad !== null ||
    estado.filtros.localidad !== null ||
    estado.filtros.estacionamientos ||
    estado.filtros.mascotas ||
    estado.filtros.caracteristicas.size > 0;

  boton_limpiar.classList.toggle('oculto', !hay_filtros_activos);
}

function manejar_checkbox_tipo(checkbox, tipo) {
  if (checkbox.checked) {
    estado.filtros.tipos.add(tipo);
  } else {
    estado.filtros.tipos.delete(tipo);
  }
  actualizar_estado_botones_filtros();
  renderizar_propiedades();
}

function actualizar_precio(deslizador) {
  const valor = parseInt(deslizador.value, 10);
  estado.filtros.precio_max = valor;

  const etiqueta = obtener_elemento('valor_precio_actual');
  if (etiqueta) etiqueta.textContent = formatear_precio(valor);

  const relleno = obtener_elemento('relleno_deslizador');
  if (relleno) relleno.style.width = valor + '%';

  actualizar_estado_botones_filtros();
  renderizar_propiedades();
}

function actualizar_filtro_area(valor) {
  const numValor = parseFloat(valor);
  estado.filtros.area_max = isNaN(numValor) ? null : numValor;

  const textoArea = obtener_elemento('valor_area_texto');
  if (textoArea) {
    textoArea.textContent = `Hasta ${Number(numValor).toLocaleString('es-CO')} m²`;
  }

  const inputRango = obtener_elemento('filtro_area');
  if (inputRango) {
    const min = parseFloat(inputRango.min) || 10;
    const max = parseFloat(inputRango.max) || 600;
    const porcentaje = ((numValor - min) / (max - min)) * 100;
    inputRango.style.setProperty('--progreso', `${porcentaje}%`);
  }

  actualizar_estado_botones_filtros();
  renderizar_propiedades();
}

function seleccionar_pastilla(pastilla_elemento) {
  const grupo = pastilla_elemento.dataset.grupo;
  const valor = pastilla_elemento.dataset.valor;
  const ya_activa = pastilla_elemento.classList.contains('activa');

  const grupo_pastillas = pastilla_elemento.closest('.grupo_pastillas');
  if (grupo_pastillas) {
    grupo_pastillas.querySelectorAll('.pastilla').forEach(p => p.classList.remove('activa'));
  }

  if (ya_activa) {
    estado.filtros[grupo] = null;
    estado.filtros[`${grupo}_es_minimo`] = false;
  } else {
    pastilla_elemento.classList.add('activa');
    const es_minimo = valor.includes('+');
    estado.filtros[grupo] = parseInt(valor.replace('+', ''), 10);
    estado.filtros[`${grupo}_es_minimo`] = es_minimo;
  }

  actualizar_estado_botones_filtros();
  renderizar_propiedades();
}

function alternar_estacionamiento(checkbox) {
  estado.filtros.estacionamientos = checkbox.checked;
  actualizar_estado_botones_filtros();
  renderizar_propiedades();
}

function alternar_mascotas(checkbox) {
  estado.filtros.mascotas = checkbox.checked;
  actualizar_estado_botones_filtros();
  renderizar_propiedades();
}

function limpiar_filtros() {
  estado.filtros.tipos.clear();
  estado.filtros.precio_max = null;
  estado.filtros.area_max = null;
  estado.filtros.habitaciones = null;
  estado.filtros.habitaciones_es_minimo = false;
  estado.filtros.banos = null;
  estado.filtros.banos_es_minimo = false;
  estado.filtros.pisos = null;
  estado.filtros.pisos_es_minimo = false;
  estado.filtros.ciudad = null;
  estado.filtros.localidad = null;
  estado.filtros.estacionamientos = false;
  estado.filtros.mascotas = false;
  estado.filtros.caracteristicas.clear();

  const select_ciudad = obtener_elemento('filtro_ciudad');
  const contenedor_localidad = obtener_elemento('contenedor_filtro_localidad');
  const select_localidad = obtener_elemento('filtro_localidad');

  if (select_ciudad) select_ciudad.value = '';
  if (contenedor_localidad) contenedor_localidad.classList.add('oculto');
  if (select_localidad) select_localidad.innerHTML = '<option value="">Todas las localidades</option>';

  const inputArea = obtener_elemento('filtro_area');
  if (inputArea) {
    inputArea.value = inputArea.max || 600;
    const textoArea = obtener_elemento('valor_area_texto');
    if (textoArea) textoArea.textContent = `Hasta ${Number(inputArea.value).toLocaleString('es-CO')} m²`;
    inputArea.style.setProperty('--progreso', '100%');
  }

  document.querySelectorAll('.checkbox_tipo').forEach(cb => cb.checked = false);
  document.querySelectorAll('.checkbox_caracteristica').forEach(cb => cb.checked = false);
  document.querySelectorAll('.pastilla').forEach(p => p.classList.remove('activa'));

  actualizar_estado_botones_filtros();
  renderizar_propiedades();
}

/* ════════════════════════════════════════════════════════════
   RENDERIZADO DE PROPIEDADES
════════════════════════════════════════════════════════════ */

function obtener_propiedades_filtradas() {
  return estado.propiedades.filter(prop => {
    /* Filtro de modo */
    if (estado.modo_navegacion !== 'todos' && prop.modo !== estado.modo_navegacion) return false;

    /* Filtro de tipo */
    if (estado.filtros.tipos.size > 0 && !estado.filtros.tipos.has(prop.tipo)) return false;

    /* Filtro de precio */
    if (estado.filtros.precio_max !== null && prop.precio > estado.filtros.precio_max) return false;

    /* Filtro de área */
    if (estado.filtros.area_max !== null && prop.area > estado.filtros.area_max) return false;

    /* Filtro habitaciones */
    if (estado.filtros.habitaciones !== null) {
      const cumple = estado.filtros.habitaciones_es_minimo
        ? prop.hab >= estado.filtros.habitaciones
        : prop.hab === estado.filtros.habitaciones;
      if (!cumple) return false;
    }

    /* Filtro baños */
    if (estado.filtros.banos !== null) {
      const cumple = estado.filtros.banos_es_minimo
        ? prop.banos >= estado.filtros.banos
        : prop.banos === estado.filtros.banos;
      if (!cumple) return false;
    }

    /* Filtro pisos */
    if (estado.filtros.pisos !== null) {
      const cumple = estado.filtros.pisos_es_minimo
        ? prop.pisos >= estado.filtros.pisos
        : prop.pisos === estado.filtros.pisos;
      if (!cumple) return false;
    }

    /* Filtro ciudad y localidad */
    if (estado.filtros.ciudad && prop.ciudad !== estado.filtros.ciudad) return false;
    if (estado.filtros.localidad && prop.localidad !== estado.filtros.localidad) return false;

    /* Filtro parqueadero / estacionamiento */
    if (estado.filtros.estacionamientos && (!prop.parqueaderos || prop.parqueaderos <= 0)) return false;

    /* Filtro mascotas */
    if (estado.filtros.mascotas && !prop.mascotas) return false;

    /* Filtro características */
    if (estado.filtros.caracteristicas.size > 0) {
      const tiene_todas = Array.from(estado.filtros.caracteristicas)
        .every(c => (prop.caracteristicas || []).includes(c));
      if (!tiene_todas) return false;
    }

    return true;
  });
}

function crear_html_tarjeta(prop) {
  const especificaciones = [];
  if (prop.hab > 0)          especificaciones.push(`${prop.hab} hab`);
  if (prop.banos > 0)        especificaciones.push(`${prop.banos} baños`);
  if (prop.pisos > 0) especificaciones.push(`${prop.pisos} pisos`);
  if (prop.area > 0)         especificaciones.push(`${prop.area}m²`);

  const espec_html = especificaciones
    .map((e, i) => i < especificaciones.length - 1
      ? `${e} <span class="separador_spec">·</span>`
      : e)
    .join(' ');

  const icono_mascota = prop.mascotas
    ? '<span class="icono_pet" title="Mascotas permitidas">🐾</span>'
    : '';

  return `
    <article class="tarjeta_propiedad" tabindex="0" aria-label="${prop.titulo}" onclick="navegar_a_detalle(${prop.id})">
      <div class="contenedor_imagen_propiedad">
        <img
          src="${prop.imagen || '../assets/placeholder.jpg'}"
          alt="${prop.titulo}"
          class="imagen_propiedad"
          loading="lazy"
        />
        <div class="degradado_imagen_propiedad" aria-hidden="true"></div>
        ${prop.badge ? `<span class="badge_propiedad">${prop.badge}</span>` : ''}
        <span class="badge_modo ${prop.modo}" aria-label="Modo: ${prop.modo}">
          ${prop.modo ? prop.modo.charAt(0).toUpperCase() + prop.modo.slice(1) : ''}
        </span>
        <p class="precio_propiedad">${prop.precio_etiqueta}</p>
        <button
          class="boton_favorito"
          aria-label="Agregar a favoritos: ${prop.titulo}"
          onclick="event.stopPropagation(); alternar_favorito(this, ${prop.id})"
        >
          <svg width="20" height="20" fill="none" stroke="currentColor" stroke-width="2" viewBox="0 0 24 24">
            <path d="M20.84 4.61a5.5 5.5 0 0 0-7.78 0L12 5.67l-1.06-1.06a5.5 5.5 0 0 0-7.78 7.78l1.06 1.06L12 21.23l7.78-7.78 1.06-1.06a5.5 5.5 0 0 0 0-7.78z"/>
          </svg>
        </button>
      </div>
      <div class="cuerpo_tarjeta">
        <h3 class="nombre_propiedad">${prop.titulo}</h3>
        <p class="ubicacion_propiedad">
          <svg width="14" height="14" fill="none" stroke="currentColor" stroke-width="2" viewBox="0 0 24 24" aria-hidden="true">
            <path d="M21 10c0 7-9 13-9 13s-9-6-9-13a9 9 0 0 1 18 0z"/>
            <circle cx="12" cy="10" r="3"/>
          </svg>
          ${prop.ubicacion || `${prop.ciudad ?? ''} ${prop.localidad ?? ''}`}
        </p>
        <p class="especificaciones_propiedad" aria-label="Especificaciones: ${especificaciones.join(', ')}">
          ${espec_html} ${icono_mascota}
        </p>
      </div>
    </article>
  `;
}

function mostrar_esqueleto_propiedades(cantidad = 6) {
  const contenedor = obtener_elemento('grilla_propiedades');
  if (!contenedor) return;

  const tarjeta_esqueleto = `
    <div class="tarjeta_esqueleto" aria-hidden="true">
      <div class="esqueleto_imagen"></div>
      <div class="esqueleto_linea esqueleto_linea--media"></div>
      <div class="esqueleto_linea esqueleto_linea--corta"></div>
    </div>
  `;

  contenedor.innerHTML = tarjeta_esqueleto.repeat(cantidad);
}

function renderizar_propiedades() {
  const contenedor = obtener_elemento('grilla_propiedades');
  const contador   = obtener_elemento('contador_resultados');
  if (!contenedor) return;

  const filtradas = obtener_propiedades_filtradas();

  if (contador) {
    const texto = filtradas.length === 1
      ? '1 propiedad encontrada'
      : `${filtradas.length} propiedades encontradas`;
    contador.textContent = texto;
  }

  if (filtradas.length === 0) {
    contenedor.innerHTML = `
      <div class="estado_vacio" style="grid-column:1/-1" role="status" aria-live="polite">
        <p class="icono_vacio" aria-hidden="true">🏘️</p>
        <p class="titulo_vacio">Sin resultados</p>
        <p class="descripcion_vacio">No encontramos propiedades con los filtros actuales.</p>
        <button class="boton_limpiar_grande" onclick="limpiar_filtros()">
          Limpiar filtros
        </button>
      </div>
    `;
    return;
  }

  contenedor.innerHTML = filtradas.map(crear_html_tarjeta).join('');
}

function alternar_favorito(boton, id_propiedad) {
  const svg = boton.querySelector('path');
  if (!svg) return;
  const activo = boton.dataset.favorito === 'true';
  boton.dataset.favorito = activo ? 'false' : 'true';
  svg.style.fill = activo ? 'none' : '#C5A365';
  svg.style.stroke = activo ? 'currentColor' : '#C5A365';
  const mensaje = activo ? 'Removido de favoritos' : 'Guardado en favoritos';
  mostrar_notificacion(mensaje, activo ? 'info' : 'exito');
}

function navegar_a_detalle(id_propiedad) {
  localStorage.setItem('filtros_activos', JSON.stringify({
    tipos: Array.from(estado.filtros.tipos),
    precio_max: estado.filtros.precio_max,
    area_max: estado.filtros.area_max,
    habitaciones: estado.filtros.habitaciones,
    banos: estado.filtros.banos,
    estacionamientos: estado.filtros.estacionamientos,
    mascotas: estado.filtros.mascotas,
    modo_navegacion: estado.modo_navegacion
  }));
  
  window.location.href = `pantalla_detalle.html?id=${id_propiedad}`;
}

/* ════════════════════════════════════════════════════════════
   INICIALIZACIÓN
════════════════════════════════════════════════════════════ */

function inicializar_app() {
  restaurar_filtros();

  document.addEventListener('keydown', evento => {
    if (evento.key === 'Escape') {
      cerrar_menu_movil();
    }
  });

  document.addEventListener('keydown', evento => {
    if (evento.key === 'Enter' && evento.target.classList.contains('tarjeta_propiedad')) {
      const tarjeta = evento.target;
      const onclickValue = tarjeta.getAttribute('onclick');
      if (onclickValue) {
        const match = onclickValue.match(/navegar_a_detalle\((\d+)\)/);
        if (match) {
          navegar_a_detalle(parseInt(match[1], 10));
        }
      }
    }
  });

  cargar_propiedades();
  cargar_filtro_tipo_inmueble();
  cargar_filtro_ubicacion();
  cargar_caracteristicas_filtro();
  verificar_autenticacion();

  // Cerrar menú de usuario al hacer clic fuera
  document.addEventListener('click', function(evento) {
    const contenedorMenu = document.querySelector('.contenedor_menu_usuario');
    const menu = document.getElementById('menu_usuario');
    
    if (contenedorMenu && menu && !menu.classList.contains('menu_usuario--oculto')) {
      if (!contenedorMenu.contains(evento.target)) {
        cerrar_menu_usuario();
      }
    }
  });
}

function restaurar_filtros() {
  const filtros_guardados = localStorage.getItem('filtros_activos');
  if (filtros_guardados) {
    try {
      const filtros = JSON.parse(filtros_guardados);
      
      if (filtros.tipos && Array.isArray(filtros.tipos)) {
        filtros.tipos.forEach(tipo => {
          estado.filtros.tipos.add(tipo);
          const checkbox = document.querySelector(`.checkbox_tipo[value="${tipo}"]`);
          if (checkbox) checkbox.checked = true;
        });
      }
      
      if (filtros.precio_max !== undefined && filtros.precio_max !== null) {
        estado.filtros.precio_max = filtros.precio_max;
        const deslizador = obtener_elemento('deslizador_precio');
        const etiqueta = obtener_elemento('valor_precio_actual');
        const relleno = obtener_elemento('relleno_deslizador');
        
        if (deslizador) deslizador.value = filtros.precio_max;
        if (etiqueta) etiqueta.textContent = formatear_precio(filtros.precio_max);
        if (relleno) relleno.style.width = filtros.precio_max + '%';
      }

      if (filtros.area_max !== undefined && filtros.area_max !== null) {
        actualizar_filtro_area(filtros.area_max);
      }
      
      ['habitaciones', 'banos', 'pisos'].forEach(grupo => {
        if (filtros[grupo] !== undefined && filtros[grupo] !== null) {
          estado.filtros[grupo] = filtros[grupo];
          const pastilla = document.querySelector(`.pastilla[data-grupo="${grupo}"][data-valor="${filtros[grupo]}"]`);
          if (pastilla) pastilla.classList.add('activa');
        }
      });

      if (filtros.estacionamientos) {
        estado.filtros.estacionamientos = true;
        const checkbox_parq = obtener_elemento('checkbox_estacionamiento');
        if (checkbox_parq) checkbox_parq.checked = true;
      }
      
      if (filtros.mascotas) {
        estado.filtros.mascotas = true;
        const checkbox_mascotas = obtener_elemento('checkbox_pet_friendly');
        if (checkbox_mascotas) checkbox_mascotas.checked = true;
      }
      
      if (filtros.modo_navegacion) {
        estado.modo_navegacion = filtros.modo_navegacion;
        const boton_modo = document.querySelector(`[data-modo="${filtros.modo_navegacion === 'todos' ? '' : filtros.modo_navegacion}"]`);
        if (boton_modo) {
          document.querySelectorAll('[data-modo]').forEach(b => b.classList.remove('activo'));
          boton_modo.classList.add('activo');
        }
        
        const titulo = obtener_elemento('titulo_seccion');
        if (titulo) {
          const titulos = {
            todos:    'Descubre tu próximo hogar',
            arriendo: 'Propiedades en arriendo',
            compra:   'Propiedades en compra',
          };
          titulo.textContent = titulos[estado.modo_navegacion] ?? titulos.todos;
        }
      }
      
      actualizar_estado_botones_filtros();
      localStorage.removeItem('filtros_activos');
    } catch (e) {
      console.error('Error al restaurar filtros:', e);
    }
  }
}

document.addEventListener('DOMContentLoaded', inicializar_app);

let mapa_leaflet = null;

/**
 * El mapa va en position: fixed (siempre del alto de la pantalla). Esta función
 * lo alinea con su columna (.panel_mapa) y lo pega justo debajo del header
 * mientras éste sea visible; cuando el header sale de pantalla, el mapa sube a 0.
 */
function posicionar_mapa_fijo() {
  const panel = document.querySelector('.panel_mapa');
  const fijo = panel ? panel.querySelector('.mapa_fijo') : null;
  if (!panel || !fijo) return;

  const rect = panel.getBoundingClientRect();
  const header = document.querySelector('.barra_navegacion');
  const base = header ? Math.max(header.getBoundingClientRect().bottom, 0) : 0;

  fijo.style.setProperty('--mapa-top', base + 'px');
  fijo.style.setProperty('--mapa-left', rect.left + 'px');
  fijo.style.setProperty('--mapa-ancho', rect.width + 'px');
}

let _mapa_raf = null;
function programar_posicion_mapa() {
  if (_mapa_raf) return;
  _mapa_raf = requestAnimationFrame(() => {
    _mapa_raf = null;
    posicionar_mapa_fijo();
  });
}

function initMap() {
  const contenedor = document.getElementById('map');
  if (!contenedor || typeof L === 'undefined') return;

  // Coordenadas iniciales (Bogotá)
  const latitud = 4.60971;
  const longitud = -74.08175;

  posicionar_mapa_fijo();
  window.addEventListener('scroll', programar_posicion_mapa, { passive: true });
  window.addEventListener('resize', programar_posicion_mapa);
  const panel_mapa = document.querySelector('.panel_mapa');
  if (panel_mapa && 'ResizeObserver' in window) {
    new ResizeObserver(programar_posicion_mapa).observe(panel_mapa);
  }

  // 1. Crear el mapa
  mapa_leaflet = L.map('map').setView([latitud, longitud], 13);

  // 2. Cargar las imágenes del mapa desde OpenStreetMap
  L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 19,
    attribution: '© OpenStreetMap'
  }).addTo(mapa_leaflet);

  // 3. Marcador de prueba
  const marker = L.marker([latitud, longitud]).addTo(mapa_leaflet);
  marker.bindPopup('<b>Inmueble de prueba</b><br>Disponible ahora.');

  // 4. Recalcular el tamaño cuando cambie el contenedor
  //    (abrir/cerrar filtros, redimensionar ventana, etc.)
  if ('ResizeObserver' in window) {
    new ResizeObserver(() => mapa_leaflet.invalidateSize()).observe(contenedor);
  } else {
    window.addEventListener('resize', () => mapa_leaflet.invalidateSize());
  }
  setTimeout(() => mapa_leaflet.invalidateSize(), 300);
}

// Ejecutar cuando se cargue la página
document.addEventListener('DOMContentLoaded', initMap);
/* ═══════════════════════════════════════════════════════
   mainpanel_administrador.js - snake_case
   ═══════════════════════════════════════════════════════ */

/* ──────────────────────────────────────────────────────
   3. TEXTOS / CONFIG
   ────────────────────────────────────────────────────── */
const config_estado_inmueble = {
  verificada: { texto: "Verificada", clase: "pildora_estado--verificada", color_punto: "#166534" },
  pendiente: { texto: "Pendiente", clase: "pildora_estado--pendiente", color_punto: "#C9A84C" },
  rechazada: { texto: "Rechazada", clase: "pildora_estado--rechazada", color_punto: "#991B1B" },
};

const config_estado_reporte = {
  abierto: { texto: "Abierto", clase: "pildora_estado--abierto", color_punto: "#991B1B" },
  revision: { texto: "En revisión", clase: "pildora_estado--revision", color_punto: "#C9A84C" },
  resuelto: { texto: "Resuelto", clase: "pildora_estado--resuelto", color_punto: "#166534" },
  descartado: { texto: "Descartado", clase: "pildora_estado--descartado", color_punto: "#6B7280" },
};

const config_req = {
  cedula: { etiqueta: "Cédula" },
  contrato: { etiqueta: "Contrato" },
  carta: { etiqueta: "Carta" },
  foto_inmueble: { etiqueta: "Foto" },
};

const config_req_estado = {
  completo: { clase: "card_usuario_movil__req--completo", icono: "✓" },
  incompleto: { clase: "card_usuario_movil__req--incompleto", icono: "✕" },
  pendiente: { clase: "card_usuario_movil__req--pendiente", icono: "⏳" },
};


/* ──────────────────────────────────────────────────────
   4. CAMBIO DE VISTA ADMIN
   ────────────────────────────────────────────────────── */
function cambiar_vista_admin(nombre_vista) {
  estado.vista_admin_actual = nombre_vista;

  // Actualizar visibilidad de las vistas
  document.querySelectorAll(".vista_admin").forEach((vista) => {
    vista.classList.toggle("vista_admin--activa", vista.id === `vista_${nombre_vista}`);
  });

  // Actualizar barra lateral
  document.querySelectorAll(".barra_lateral__item").forEach((item) => {
    item.classList.toggle("barra_lateral__item--activo", item.dataset.vista === nombre_vista);
  });

  // Actualizar nav inferior móvil
  document.querySelectorAll(".nav_inferior_movil__item").forEach((item) => {
    item.classList.toggle("nav_inferior_movil__item--activo", item.dataset.vista === nombre_vista);
  });

  // Actualizar encabezado
  actualizar_encabezado_admin(nombre_vista);

  // Renderizar según la vista
  switch (nombre_vista) {
    case "tablero": renderizar_tablero(); break;
    case "inmuebles": renderizar_inmuebles(); break;
    case "reportes": renderizar_reportes(); break;
    case "usuarios": renderizar_usuarios(); break;
    case "vendedor_arrendatario": renderizar_vendedor_arrendatario(); break;
  }
}

function actualizar_encabezado_admin(vista) {
  const titulos = {
    tablero: "Panel de administración",
    inmuebles: "Gestión de inmuebles",
    detalle: "Detalle del inmueble",
    reportes: "Reportes",
    usuarios: "Usuarios registrados",
    vendedor_arrendatario: "Vendedores y Arrendatarios",
  };

  const el_titulo = document.getElementById("titulo_vista_actual");
  const el_volver = document.getElementById("boton_volver");

  if (el_titulo) el_titulo.textContent = titulos[vista] || "";
  if (el_volver) {
    el_volver.style.display = vista === "detalle" ? "flex" : "none";
  }
}


/* ──────────────────────────────────────────────────────
   5. RENDERIZAR TABLERO
   ────────────────────────────────────────────────────── */
function renderizar_tablero() {
  const total_verificadas = inmuebles.filter((i) => i.estado === "verificada").length;
  const total_pendientes = inmuebles.filter((i) => i.estado === "pendiente").length;
  const total_rechazadas = inmuebles.filter((i) => i.estado === "rechazada").length;
  const total_inmuebles = inmuebles.length;
  const total_reportes = reportes.filter((r) => r.estado === "abierto").length;

  const el = (id, val) => {
    const node = document.getElementById(id);
    if (node) node.textContent = val;
  };

  el("stat_verificadas", total_verificadas);
  el("stat_pendientes", total_pendientes);
  el("stat_rechazadas", total_rechazadas);
  el("stat_total", total_inmuebles);
  el("stat_reportes", total_reportes);

  // Lista de pendientes recientes
  const contenedor_pendientes = document.getElementById("lista_pendientes_tablero");
  if (contenedor_pendientes) {
    const pendientes = inmuebles.filter((i) => i.estado === "pendiente");
    contenedor_pendientes.innerHTML = pendientes.length
      ? pendientes.map((i) => `
        <li class="lista_pendientes__item" onclick="ver_detalle(${i.id})" role="button" tabindex="0">
          <div class="lista_pendientes__foto">
            <img src="${i.foto}" alt="${i.nombre}" loading="lazy" />
          </div>
          <div class="lista_pendientes__info">
            <p class="lista_pendientes__nombre">${i.nombre}</p>
            <p class="lista_pendientes__meta">${i.ciudad} · ${i.tipo}</p>
          </div>
          <span class="lista_pendientes__fecha">${i.fecha}</span>
        </li>
      `).join("")
      : '<li style="padding:1rem 1.25rem;font-size:0.875rem;color:#9CA3AF;">Sin inmuebles pendientes</li>';
  }
}
// 1. Manejador del evento del input (actualiza el estado y dispara la vista)
function manejar_cambio_buscador_inmuebles() {
    const input = document.getElementById("buscador_inmuebles");
    if (!input) return;

    estado.busqueda_inmuebles = input.value;
    renderizar_inmuebles();
}

// 2. Función pura de lógica de datos (calcula y devuelve el arreglo filtrado)
function obtener_inmuebles_filtrados() {
    return inmuebles.filter((i) => {
        const coincide_filtro = estado.filtro_inmuebles === "todos" || i.estado === estado.filtro_inmuebles;
        const termino = estado.busqueda_inmuebles.toLowerCase();
        
        const coincide_busqueda = !termino ||
            i.nombre.toLowerCase().includes(termino) ||
            i.ciudad.toLowerCase().includes(termino) ||
            i.propietario.toLowerCase().includes(termino);
            
        return coincide_filtro && coincide_busqueda;
    });
}
const busq_inmuebles = document.getElementById("buscador_inmuebles");
if (busq_inmuebles) {
    busq_inmuebles.addEventListener("input", manejar_cambio_buscador_inmuebles);
}
function renderizar_inmuebles() {
    // 1. Obtenemos la lista procesada (aquí ya estamos usando la variable)
    const lista_filtrada = obtener_inmuebles_filtrados();

    // 2. Buscamos el contenedor donde se van a mostrar en el HTML
    const contenedor = document.getElementById("contenedor_tarjetas_inmuebles");
    if (!contenedor) return;

    // 3. Limpiamos el contenedor antes de renderizar de nuevo
    contenedor.innerHTML = "";

    // 4. Si no hay resultados, mostramos un mensaje amigable
    if (lista_filtrada.length === 0) {
        contenedor.innerHTML = "<p>No se encontraron inmuebles con esos criterios.</p>";
        return;
    }

    // 5. Recorremos la lista filtrada y creamos el HTML para cada inmueble
    lista_filtrada.forEach(inmueble => {
        const tarjeta = document.createElement("div");
        tarjeta.className = "tarjeta_inmueble";
        tarjeta.innerHTML = `
            <h3>${inmueble.nombre}</h3>
            <p>Ciudad: ${inmueble.ciudad}</p>
            <p>Propietario: ${inmueble.propietario}</p>
        `;
        contenedor.appendChild(tarjeta);
    });
}
function obtener_inmuebles_filtrados() {
    return inmuebles.filter((i) => {
        const coincide_filtro = estado.filtro_inmuebles === "todos" || i.estado === estado.filtro_inmuebles;
        const termino = estado.busqueda_inmuebles.toLowerCase();

        const coincide_busqueda = !termino ||
            i.nombre.toLowerCase().includes(termino) ||
            i.ciudad.toLowerCase().includes(termino) ||
            i.propietario.toLowerCase().includes(termino);

        return coincide_filtro && coincide_busqueda;
    });
}
function renderizar_inmuebles() {
    const filtrados = obtener_inmuebles_filtrados();

    renderizar_tabla_inmuebles(filtrados);
    renderizar_cards_inmuebles(filtrados);
}
function manejar_cambio_buscador_inmuebles() {
    const input = document.getElementById("buscador_inmuebles");
    if (!input) return;

    estado.busqueda_inmuebles = input.value;
    renderizar_inmuebles();
}
function renderizar_tabla_inmuebles(lista) {
  const tbody = document.getElementById("cuerpo_tabla_inmuebles");
  if (!tbody) return;

  tbody.innerHTML = lista.map((i) => {
    const cfg = config_estado_inmueble[i.estado] || {};
    return `
      <tr>
        <td>
          <div class="celda_inmueble">
            <div class="celda_inmueble__foto">
              <img src="${i.foto}" alt="${i.nombre}" loading="lazy" />
            </div>
            <div>
              <p class="celda_inmueble__nombre">${i.nombre}</p>
              <p class="celda_inmueble__ubicacion">${i.ciudad} · ${i.tipo}</p>
            </div>
          </div>
        </td>
        <td>
          <p>${i.propietario}</p>
          <p class="celda_meta">${i.correo_prop}</p>
        </td>
        <td class="celda_precio">${i.precio}<br><span class="celda_meta">/mes</span></td>
        <td>
          <span class="pildora_estado ${cfg.clase}">
            <span class="pildora_estado__punto" style="background-color:${cfg.color_punto}"></span>
            ${cfg.texto}
          </span>
        </td>
        <td class="celda_meta">${i.fecha}</td>
        <td>
          <div class="celda_acciones">
            <button class="boton_accion boton_accion--ver" onclick="ver_detalle(${i.id})" title="Ver detalle" aria-label="Ver inmueble ${i.nombre}">
              <img src="../assets/iconos/documento.svg" alt="" />
            </button>
            <button class="boton_accion boton_accion--editar" onclick="editar_inmueble(${i.id})" title="Editar" aria-label="Editar inmueble ${i.nombre}">
              <img src="../assets/iconos/regla.svg" alt="" />
            </button>
            <button class="boton_accion boton_accion--eliminar" onclick="confirmar_eliminar_inmueble(${i.id})" title="Eliminar" aria-label="Eliminar inmueble ${i.nombre}">
              <img src="../assets/iconos/cerrar.svg" alt="" />
            </button>
          </div>
        </td>
      </tr>
    `;
  }).join("") || `<tr><td colspan="6" style="text-align:center;padding:1.5rem;color:#9CA3AF;font-size:0.875rem;">Sin resultados</td></tr>`;
}

function renderizar_cards_inmuebles(lista) {
  const contenedor = document.getElementById("cards_inmuebles_movil");
  if (!contenedor) return;

  contenedor.innerHTML = lista.map((i) => {
    const cfg = config_estado_inmueble[i.estado] || {};
    return `
      <article class="card_movil">
        <div class="card_movil__cabecera">
          <div class="card_movil__foto">
            <img src="${i.foto}" alt="${i.nombre}" loading="lazy" />
          </div>
          <div>
            <p class="card_movil__nombre">${i.nombre}</p>
            <p class="card_movil__sub">${i.ciudad} · ${i.tipo}</p>
          </div>
        </div>
        <div class="card_movil__fila">
          <span class="card_movil__fila_label">Estado</span>
          <span class="pildora_estado ${cfg.clase}">
            <span class="pildora_estado__punto" style="background-color:${cfg.color_punto}"></span>
            ${cfg.texto}
          </span>
        </div>
        <div class="card_movil__fila">
          <span class="card_movil__fila_label">Precio</span>
          <span class="card_movil__precio">${i.precio}/mes</span>
        </div>
        <div class="card_movil__fila">
          <span class="card_movil__fila_label">Propietario</span>
          <span>${i.propietario}</span>
        </div>
        <div class="card_movil__acciones">
          <button class="card_movil__boton card_movil__boton--ver" onclick="ver_detalle(${i.id})">
            <img src="../assets/iconos/documento.svg" alt="" /> Ver
          </button>
          <button class="card_movil__boton card_movil__boton--editar" onclick="editar_inmueble(${i.id})">
            <img src="../assets/iconos/regla.svg" alt="" /> Editar
          </button>
          <button class="card_movil__boton card_movil__boton--eliminar" onclick="confirmar_eliminar_inmueble(${i.id})">
            <img src="../assets/iconos/cerrar.svg" alt="" /> Eliminar
          </button>
        </div>
      </article>
    `;
  }).join("") || `<p style="text-align:center;padding:1.5rem;color:#9CA3AF;font-size:0.875rem;">Sin resultados</p>`;
}

function aplicar_filtro_estado(filtro, boton) {
  estado.filtro_inmuebles = filtro;
  document.querySelectorAll(".boton_filtro").forEach((btn) => {
    btn.classList.remove("boton_filtro--activo");
  });
  boton.classList.add("boton_filtro--activo");
  renderizar_inmuebles();
}

/* ──────────────────────────────────────────────────────
   7. VER DETALLE DE INMUEBLE
   ────────────────────────────────────────────────────── */
function ver_detalle(inmueble_id) {
  estado.inmueble_detalle_id = inmueble_id;
  renderizar_detalle(inmueble_id);
  cambiar_vista_admin("detalle");
}

function volver_a_listado() {
  cambiar_vista_admin("inmuebles");
}

function renderizar_detalle(inmueble_id) {
  const inmueble = inmuebles.find((i) => i.id === inmueble_id);
  if (!inmueble) return;

  const vista = document.getElementById("vista_detalle");
  if (!vista) return;

  const cfg = config_estado_inmueble[inmueble.estado] || {};

  vista.innerHTML = `
    <!-- Estado y acciones -->
    <div class="panel_detalle_estado">
      <div class="panel_detalle_estado__fila">
        <span class="pildora_estado ${cfg.clase}" style="font-size:0.875rem;padding:0.375rem 0.875rem;">
          <span class="pildora_estado__punto" style="background-color:${cfg.color_punto}"></span>
          ${cfg.texto}
        </span>
        <div class="panel_detalle_estado__acciones">
          <button class="boton_accion_detalle boton_accion_detalle--verificar" onclick="cambiar_estado_inmueble(${inmueble_id},'verificada')">
            <img src="../assets/iconos/verificado.svg" alt="" /> Verificar
          </button>
          <button class="boton_accion_detalle boton_accion_detalle--rechazar" onclick="cambiar_estado_inmueble(${inmueble_id},'rechazada')">
            <img src="../assets/iconos/cerrar.svg" alt="" /> Rechazar
          </button>
          <button class="boton_accion_detalle boton_accion_detalle--eliminar" onclick="confirmar_eliminar_inmueble(${inmueble_id})">
            <img src="../assets/iconos/cerrar.svg" alt="" /> Eliminar
          </button>
        </div>
      </div>
      <div class="nota_admin">
        <p class="nota_admin__etiqueta">Nota del administrador</p>
        <textarea class="nota_admin__textarea" rows="2" placeholder="Escribe una nota sobre este inmueble..." id="nota_admin_${inmueble_id}"></textarea>
      </div>
    </div>

    <!-- Cuadrícula detalle -->
    <div class="cuadricula_detalle">

      <!-- Columna principal -->
      <div class="cuadricula_detalle__principal" style="display:flex;flex-direction:column;gap:1rem;">

        <!-- Fotos -->
        <div class="panel_blanco">
          <div class="panel_blanco__encabezado">
            <h3 class="panel_blanco__titulo">Galería de fotos</h3>
            <button class="boton_agregar_foto">+ Agregar</button>
          </div>
          <div class="panel_blanco__cuerpo">
            <div class="galeria_admin">
              ${inmueble.fotos.map((foto, idx) => `
                <div class="galeria_admin__item">
                  <img src="${foto}" alt="Foto ${idx + 1}" class="galeria_admin__imagen" loading="lazy" />
                  ${idx === 0 ? '<span class="galeria_admin__etiqueta">Principal</span>' : ""}
                  <div class="galeria_admin__superposicion">
                    <button class="galeria_admin__boton" title="Eliminar foto">
                      <img src="../assets/iconos/cerrar.svg" alt="Eliminar" />
                    </button>
                  </div>
                </div>
              `).join("")}
            </div>
          </div>
        </div>

        <!-- Descripción -->
        <div class="panel_blanco">
          <div class="panel_blanco__encabezado">
            <h3 class="panel_blanco__titulo">Descripción</h3>
          </div>
          <div class="panel_blanco__cuerpo">
            <p style="font-size:0.875rem;line-height:1.7;color:#4B5563;">${inmueble.descripcion}</p>
            <div class="grupo_chips_amenidad">
              ${inmueble.amenidades.map((a) => `<span class="chip_amenidad_detalle">${a}</span>`).join("")}
            </div>
          </div>
        </div>
      </div>

      <!-- Columna lateral -->
      <div class="cuadricula_detalle__lateral" style="display:flex;flex-direction:column;gap:1rem;">

        <!-- Información general -->
        <div class="panel_blanco">
          <div class="panel_blanco__encabezado">
            <h3 class="panel_blanco__titulo">Información</h3>
          </div>
          <div class="panel_blanco__cuerpo">
            <div class="lista_detalles">
              <div class="lista_detalles__item">
                <img src="../assets/iconos/mapa.svg" alt="" class="lista_detalles__icono" />
                <div>
                  <p class="lista_detalles__etiqueta">Ciudad</p>
                  <p class="lista_detalles__valor">${inmueble.ciudad}</p>
                </div>
              </div>
              <div class="lista_detalles__item">
                <img src="../assets/iconos/mapa.svg" alt="" class="lista_detalles__icono" />
                <div>
                  <p class="lista_detalles__etiqueta">Dirección</p>
                  <p class="lista_detalles__valor">${inmueble.direccion}</p>
                </div>
              </div>
              <div class="lista_detalles__item">
                <img src="../assets/iconos/edificio.svg" alt="" class="lista_detalles__icono" />
                <div>
                  <p class="lista_detalles__etiqueta">Tipo</p>
                  <p class="lista_detalles__valor">${inmueble.tipo}</p>
                </div>
              </div>
              <div class="lista_detalles__item">
                <img src="../assets/iconos/regla.svg" alt="" class="lista_detalles__icono" />
                <div>
                  <p class="lista_detalles__etiqueta">Área</p>
                  <p class="lista_detalles__valor">${inmueble.area} m²</p>
                </div>
              </div>
              <div class="lista_detalles__item">
                <img src="../assets/iconos/dinero.svg" alt="" class="lista_detalles__icono" />
                <div>
                  <p class="lista_detalles__etiqueta">Precio/mes</p>
                  <p class="lista_detalles__valor">${inmueble.precio}</p>
                </div>
              </div>
              ${inmueble.habitaciones > 0 ? `
              <div class="lista_detalles__item">
                <img src="../assets/iconos/casa.svg" alt="" class="lista_detalles__icono" />
                <div>
                  <p class="lista_detalles__etiqueta">Habitaciones</p>
                  <p class="lista_detalles__valor">${inmueble.habitaciones}</p>
                </div>
              </div>` : ""}
              <div class="lista_detalles__item">
                <img src="../assets/iconos/info.svg" alt="" class="lista_detalles__icono" />
                <div>
                  <p class="lista_detalles__etiqueta">Baños</p>
                  <p class="lista_detalles__valor">${inmueble.banos}</p>
                </div>
              </div>
            </div>
          </div>
        </div>

        <!-- Propietario -->
        <div class="panel_blanco">
          <div class="panel_blanco__encabezado">
            <h3 class="panel_blanco__titulo">Propietario</h3>
          </div>
          <div class="panel_blanco__cuerpo">
            <div class="info_propietario">
              <div class="info_propietario__avatar">${inmueble.propietario.split(" ").map((p) => p[0]).join("").slice(0, 2)}</div>
              <div>
                <p class="info_propietario__nombre">${inmueble.propietario}</p>
                <p class="info_propietario__cargo">Propietario</p>
              </div>
            </div>
            <div class="info_propietario__contacto">
              <div class="info_propietario__linea">
                <img src="../assets/iconos/correo.svg" alt="" />
                <span>${inmueble.correo_prop}</span>
              </div>
              <div class="info_propietario__linea">
                <img src="../assets/iconos/telefono.svg" alt="" />
                <span>${inmueble.telefono_prop}</span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  `;
}

function cambiar_estado_inmueble(inmueble_id, nuevo_estado) {
  const idx = inmuebles.findIndex((i) => i.id === inmueble_id);
  if (idx !== -1) {
    inmuebles[idx].estado = nuevo_estado;
    renderizar_detalle(inmueble_id);
    renderizar_tablero();
  }
}

function editar_inmueble(inmueble_id) {
  ver_detalle(inmueble_id);
}

function confirmar_eliminar_inmueble(inmueble_id) {
  const inmueble = inmuebles.find((i) => i.id === inmueble_id);
  if (!inmueble) return;
  if (confirm(`¿Eliminar "${inmueble.nombre}"? Esta acción no se puede deshacer.`)) {
    const idx = inmuebles.findIndex((i) => i.id === inmueble_id);
    if (idx !== -1) inmuebles.splice(idx, 1);
    cambiar_vista_admin("inmuebles");
  }
}


/* ──────────────────────────────────────────────────────
   8. RENDERIZAR REPORTES
   ────────────────────────────────────────────────────── */
function renderizar_reportes() {
  actualizar_resumen_reportes();
  renderizar_lista_reportes();
}

function actualizar_resumen_reportes() {
  const conteos = { abierto: 0, revision: 0, resuelto: 0, descartado: 0 };
  reportes.forEach((r) => { if (conteos[r.estado] !== undefined) conteos[r.estado]++; });

  const el = (id, val) => { const n = document.getElementById(id); if (n) n.textContent = val; };
  el("cnt_abierto", conteos.abierto);
  el("cnt_revision", conteos.revision);
  el("cnt_resuelto", conteos.resuelto);
  el("cnt_descartado", conteos.descartado);
}

function renderizar_lista_reportes() {
  const contenedor = document.getElementById("lista_reportes");
  if (!contenedor) return;

  const filtrados = reportes.filter((r) => {
    return estado.filtro_reportes === "todos" || r.estado === estado.filtro_reportes;
  });

  contenedor.innerHTML = filtrados.map((r) => {
    const cfg = config_estado_reporte[r.estado] || {};
    const estados_botones = ["abierto", "revision", "resuelto", "descartado"];
    return `
      <article class="tarjeta_reporte ${r.expandido ? 'tarjeta_reporte--expandida' : ''}" id="reporte_${r.id}">
        <div class="tarjeta_reporte__cabecera" onclick="alternar_reporte(${r.id})" role="button" tabindex="0" aria-expanded="${r.expandido}">
          <div class="tarjeta_reporte__icono_tipo">
            <img src="../assets/iconos/alerta.svg" alt="" />
          </div>
          <div class="tarjeta_reporte__info">
            <p class="tarjeta_reporte__tipo">${r.tipo}</p>
            <p class="tarjeta_reporte__meta">${r.inmueble} · Reportado por ${r.reportante}</p>
          </div>
          <span class="pildora_estado ${cfg.clase}">
            <span class="pildora_estado__punto" style="background-color:${cfg.color_punto}"></span>
            ${cfg.texto}
          </span>
          <span class="tarjeta_reporte__fecha">${r.fecha}</span>
        </div>
        <div class="tarjeta_reporte__cuerpo ${r.expandido ? 'tarjeta_reporte__cuerpo--visible' : ''}">
          <div class="cuadricula_reporte">
            <div>
              <div class="bloque_descripcion">
                <p class="bloque_descripcion__etiqueta">Descripción del reporte</p>
                <p class="bloque_descripcion__texto">${r.descripcion}</p>
              </div>
              ${r.evidencia ? `
              <div class="evidencia_foto" style="margin-top:0.75rem;">
                <img src="${r.evidencia}" alt="Evidencia" loading="lazy" />
              </div>` : ""}
            </div>
            <div class="panel_acciones_reporte">
              <div class="bloque_reportante">
                <p class="bloque_reportante__nombre">${r.reportante}</p>
                <p class="bloque_reportante__correo">${r.correo_rep}</p>
              </div>
              <div class="botones_estado_reporte">
                ${estados_botones.map((est) => {
      const c = config_estado_reporte[est];
      return `<button class="boton_estado_reporte boton_estado_reporte--${est} ${r.estado === est ? 'boton_estado_reporte--activo' : ''}" onclick="cambiar_estado_reporte(${r.id},'${est}')">${c.texto}</button>`;
    }).join("")}
              </div>
              <button class="boton_ver_inmueble" onclick="ver_detalle(${r.inmueble_id})">
                <img src="../assets/iconos/edificio.svg" alt="" /> Ver inmueble
              </button>
            </div>
          </div>
        </div>
      </article>
    `;
  }).join("") || `<p style="text-align:center;padding:1.5rem;color:#9CA3AF;font-size:0.875rem;">Sin reportes para el filtro seleccionado.</p>`;
}

function alternar_reporte(reporte_id) {
  const idx = reportes.findIndex((r) => r.id === reporte_id);
  if (idx !== -1) {
    reportes[idx].expandido = !reportes[idx].expandido;
    renderizar_lista_reportes();
  }
}

function cambiar_estado_reporte(reporte_id, nuevo_estado) {
  const idx = reportes.findIndex((r) => r.id === reporte_id);
  if (idx !== -1) {
    reportes[idx].estado = nuevo_estado;
    renderizar_reportes();
  }
}


/* ──────────────────────────────────────────────────────
   9. RENDERIZAR USUARIOS
   ────────────────────────────────────────────────────── */
function renderizar_usuarios() {
  const filtrados = filtrar_usuarios_lista();
  renderizar_tabla_usuarios(filtrados);
  renderizar_cards_usuarios(filtrados);
}

function filtrar_usuarios() {
  return usuarios.filter((u) => {
    const coincide_filtro = estado.filtro_usuarios === "todos" || u.tipo === estado.filtro_usuarios;
    const termino = estado.busqueda_usuarios.toLowerCase();
    const coincide_busqueda = !termino ||
      u.nombre.toLowerCase().includes(termino) ||
      u.correo.toLowerCase().includes(termino);
    return coincide_filtro && coincide_busqueda;
  });
}

function renderizar_tabla_usuarios(lista) {
  const tbody = document.getElementById("cuerpo_tabla_usuarios");
  if (!tbody) return;

  const req_keys = Object.keys(config_req);

  tbody.innerHTML = lista.map((u) => {
    const tipo_cfg = u.tipo === "propietario"
      ? { clase: "pildora_estado--propietario", texto: "Propietario" }
      : { clase: "pildora_estado--comprador", texto: "Comprador" };

    const cedula_formato = `${u.tipo_documento || 'cc'} ${u.numero_documento || 'No registrada'}`;

    return `
      <tr>
        <td>
          <div class="celda_usuario">
            <div class="celda_usuario__avatar">${u.iniciales}</div>
            <div>
              <p class="celda_usuario__nombre">${u.nombre}</p>
              <p class="celda_usuario__correo">${u.correo}</p>
            </div>
          </div>
        </td>
        <td>
          <span class="pildora_estado ${tipo_cfg.clase}">
            <span class="pildora_estado__punto" style="background-color:${u.tipo === "propietario" ? "#2C2C2C" : "#C9A84C"}"></span>
            ${tipo_cfg.texto}
          </span>
        </td>
        <td class="celda_meta">${cedula_formato}</td>
        <td class="celda_meta">${u.correo}</td>
        <td class="celda_meta">${u.telefono || '--'}</td>
        <td>
          <div class="celda_acciones">
            <button class="boton_accion boton_accion--ver" onclick="ver_usuario(${u.id})" title="Ver usuario" aria-label="Ver usuario ${u.nombre}">
              <img src="../assets/iconos/usuario.svg" alt="" />
            </button>
            <button class="boton_accion boton_accion--eliminar" onclick="eliminar_usuario(${u.id})" title="Eliminar usuario" aria-label="Eliminar usuario ${u.nombre}">
              <img src="../assets/iconos/cerrar.svg" alt="" />
            </button>
          </div>
        </td>
      </tr>
    `;
  }).join("") || `<tr><td colspan="6" style="text-align:center;padding:1.5rem;color:#9CA3AF;font-size:0.875rem;">Sin usuarios</td></tr>`;
}

function renderizar_cards_usuarios(lista) {
  const contenedor = document.getElementById("cards_usuarios_movil");
  if (!contenedor) return;

  contenedor.innerHTML = lista.map((u) => {
    const tipo_texto = u.tipo === "propietario" ? "Propietario" : "Comprador";
    const cedula_formato = `${u.tipo_documento || 'cc'} ${u.numero_documento || 'No registrada'}`;

    return `
      <article class="card_usuario_movil">
        <div class="card_usuario_movil__cabecera">
          <div class="card_usuario_movil__avatar">${u.iniciales}</div>
          <div>
            <p class="card_usuario_movil__nombre">${u.nombre}</p>
            <p class="card_usuario_movil__correo">${u.correo} · ${tipo_texto}</p>
          </div>
        </div>
        <div class="card_usuario_movil__requisitos">
          <span class="card_usuario_movil__req card_usuario_movil__req--completo">Cédula: ${cedula_formato}</span>
          <span class="card_usuario_movil__req card_usuario_movil__req--completo">Tel: ${u.telefono || '--'}</span>
        </div>
        <div class="card_movil__acciones">
          <button class="card_movil__boton card_movil__boton--ver" onclick="ver_usuario(${u.id})">
            <img src="../assets/iconos/usuario.svg" alt="" /> Ver
          </button>
          <button class="card_movil__boton card_movil__boton--eliminar" onclick="eliminar_usuario(${u.id})">
            <img src="../assets/iconos/cerrar.svg" alt="" /> Eliminar
          </button>
        </div>
      </article>
    `;
  }).join("") || `<p style="text-align:center;padding:1.5rem;color:#9CA3AF;font-size:0.875rem;">Sin usuarios</p>`;
}

function handle_filtrar_usuarios() {
  const input = document.getElementById("buscador_usuarios");
  if (input) {
    estado.busqueda_usuarios = input.value;
    renderizar_usuarios();
  }
}

function filtrar_usuarios_lista() {
  return usuarios.filter((u) => {
    const coincide_filtro = estado.filtro_usuarios === "todos" || u.tipo === estado.filtro_usuarios;
    const termino = estado.busqueda_usuarios.toLowerCase();
    const coincide_busqueda = !termino ||
      u.nombre.toLowerCase().includes(termino) ||
      u.correo.toLowerCase().includes(termino);
    return coincide_filtro && coincide_busqueda;
  });
}

// CRUD functions for Usuarios
function ver_usuario(usuario_id) {
  const usuario = usuarios.find(u => u.id === usuario_id);
  if (usuario) {
    const cedula_formato = `${usuario.tipo_documento || 'cc'} ${usuario.numero_documento || 'No registrada'}`;
    alert(`Usuario: ${usuario.nombre}\nCorreo: ${usuario.correo}\nTipo: ${usuario.tipo}\nCédula: ${cedula_formato}\nTeléfono: ${usuario.telefono || 'No registrado'}\nInmuebles: ${usuario.inmuebles}\nRegistro: ${usuario.registro}`);
  }
}

function eliminar_usuario(usuario_id) {
  const usuario = usuarios.find(u => u.id === usuario_id);
  if (usuario && confirm(`¿Eliminar usuario "${usuario.nombre}"? Esta acción no se puede deshacer.`)) {
    const idx = usuarios.findIndex(u => u.id === usuario_id);
    if (idx !== -1) {
      usuarios.splice(idx, 1);
      renderizar_usuarios();
    }
  }
}

/* ──────────────────────────────────────────────────────
   11. RENDERIZAR VENDEDOR/ARRENDATARIO
   ────────────────────────────────────────────────────── */
function renderizar_vendedor_arrendatario() {
  const filtrados = filtrar_vendedor_arrendatario_lista();
  renderizar_tabla_vendedor_arrendatario(filtrados);
  renderizar_cards_vendedor_arrendatario(filtrados);
}

function filtrar_vendedor_arrendatario_lista() {
  return vendedor_arrendatario.filter((va) => {
    const coincide_filtro = estado.filtro_vendedor_arrendatario === "todos" || va.tipo === estado.filtro_vendedor_arrendatario || va.estado === estado.filtro_vendedor_arrendatario;
    const termino = estado.busqueda_vendedor_arrendatario.toLowerCase();
    const coincide_busqueda = !termino ||
      va.nombre.toLowerCase().includes(termino) ||
      va.correo.toLowerCase().includes(termino) ||
      va.cedula.includes(termino);
    return coincide_filtro && coincide_busqueda;
  });
}

function renderizar_tabla_vendedor_arrendatario(lista) {
  const tbody = document.getElementById("cuerpo_tabla_vendedor_arrendatario");
  if (!tbody) return;

  tbody.innerHTML = lista.map((va) => {
    const tipo_cfg = va.tipo === "vendedor"
      ? { clase: "pildora_estado--propietario", texto: "Vendedor" }
      : { clase: "pildora_estado--comprador", texto: "Arrendatario" };

    const estado_cfg = {
      activo: { clase: "pildora_estado--verificada", texto: "Activo" },
      inactivo: { clase: "pildora_estado--rechazada", texto: "Inactivo" },
      pendiente: { clase: "pildora_estado--pendiente", texto: "Pendiente" }
    };

    const estado_config = estado_cfg[va.estado] || estado_cfg.activo;

    return `
      <tr>
        <td>
          <div class="celda_usuario">
            <div class="celda_usuario__avatar">${va.iniciales}</div>
            <div>
              <p class="celda_usuario__nombre">${va.nombre}</p>
              <p class="celda_usuario__correo">${va.correo}</p>
            </div>
          </div>
        </td>
        <td>
          <span class="pildora_estado ${tipo_cfg.clase}">
            <span class="pildora_estado__punto" style="background-color:${va.tipo === "vendedor" ? "#2C2C2C" : "#C9A84C"}"></span>
            ${tipo_cfg.texto}
          </span>
        </td>
        <td class="celda_meta">${va.cedula}</td>
        <td class="celda_meta">${va.correo}</td>
        <td class="celda_meta">${va.telefono}</td>
        <td class="celda_meta">${va.direccion}</td>
        <td class="celda_meta">${va.inmuebles} inmueble${va.inmuebles !== 1 ? "s" : ""}</td>
        <td>
          <span class="pildora_estado ${estado_config.clase}">
            <span class="pildora_estado__punto" style="background-color:${va.estado === 'activo' ? '#166534' : va.estado === 'inactivo' ? '#991B1B' : '#C9A84C'}"></span>
            ${estado_config.texto}
          </span>
        </td>
        <td>
          <div class="celda_acciones">
            <button class="boton_accion boton_accion--ver" onclick="ver_vendedor_arrendatario(${va.id})" title="Ver" aria-label="Ver ${va.nombre}">
              <img src="../assets/iconos/usuario.svg" alt="" />
            </button>
            <button class="boton_accion boton_accion--editar" onclick="editar_vendedor_arrendatario(${va.id})" title="Editar" aria-label="Editar ${va.nombre}">
              <img src="../assets/iconos/regla.svg" alt="" />
            </button>
            <button class="boton_accion boton_accion--eliminar" onclick="eliminar_vendedor_arrendatario(${va.id})" title="Eliminar" aria-label="Eliminar ${va.nombre}">
              <img src="../assets/iconos/cerrar.svg" alt="" />
            </button>
          </div>
        </td>
      </tr>
    `;
  }).join("") || `<tr><td colspan="9" style="text-align:center;padding:1.5rem;color:#9CA3AF;font-size:0.875rem;">Sin vendedores/arrendatarios</td></tr>`;
}

function renderizar_cards_vendedor_arrendatario(lista) {
  const contenedor = document.getElementById("cards_vendedor_arrendatario_movil");
  if (!contenedor) return;

  const estado_cfg = {
    activo: { clase: "pildora_estado--verificada", texto: "Activo" },
    inactivo: { clase: "pildora_estado--rechazada", texto: "Inactivo" },
    pendiente: { clase: "pildora_estado--pendiente", texto: "Pendiente" }
  };

  contenedor.innerHTML = lista.map((va) => {
    const tipo_texto = va.tipo === "vendedor" ? "Vendedor" : "Arrendatario";
    const estado_config = estado_cfg[va.estado] || estado_cfg.activo;

    return `
      <article class="card_usuario_movil">
        <div class="card_usuario_movil__cabecera">
          <div class="card_usuario_movil__avatar">${va.iniciales}</div>
          <div>
            <p class="card_usuario_movil__nombre">${va.nombre}</p>
            <p class="card_usuario_movil__correo">${va.correo} · ${tipo_texto}</p>
          </div>
        </div>
        <div class="card_usuario_movil__requisitos">
          <span class="card_usuario_movil__req card_usuario_movil__req--completo">Cédula: ${va.cedula}</span>
          <span class="card_usuario_movil__req card_usuario_movil__req--completo">Tel: ${va.telefono}</span>
          <span class="card_usuario_movil__req card_usuario_movil__req--completo">Inmuebles: ${va.inmuebles}</span>
        </div>
        <div class="card_movil__acciones">
          <button class="card_movil__boton card_movil__boton--ver" onclick="ver_vendedor_arrendatario(${va.id})">
            <img src="../assets/iconos/usuario.svg" alt="" /> Ver
          </button>
          <button class="card_movil__boton card_movil__boton--editar" onclick="editar_vendedor_arrendatario(${va.id})">
            <img src="../assets/iconos/regla.svg" alt="" /> Editar
          </button>
          <button class="card_movil__boton card_movil__boton--eliminar" onclick="eliminar_vendedor_arrendatario(${va.id})">
            <img src="../assets/iconos/cerrar.svg" alt="" /> Eliminar
          </button>
        </div>
      </article>
    `;
  }).join("") || `<p style="text-align:center;padding:1.5rem;color:#9CA3AF;font-size:0.875rem;">Sin vendedores/arrendatarios</p>`;
}

function filtrar_vendedor_arrendatario() {
  const input = document.getElementById("buscador_vendedor_arrendatario");
  if (input) {
    estado.busqueda_vendedor_arrendatario = input.value;
    renderizar_vendedor_arrendatario();
  }
}

// CRUD functions for Vendedor/Arrendatario
function ver_vendedor_arrendatario(id) {
  const va = vendedor_arrendatario.find(v => v.id === id);
  if (va) {
    alert(`Vendedor/Arrendatario: ${va.nombre}\nTipo: ${va.tipo}\nCédula: ${va.cedula}\nCorreo: ${va.correo}\nTeléfono: ${va.telefono}\nDirección: ${va.direccion}\nEstado: ${va.estado}\nInmuebles: ${va.inmuebles}\nObservaciones: ${va.observaciones || 'Ninguna'}`);
  }
}

function editar_vendedor_arrendatario(id) {
  const va = vendedor_arrendatario.find(v => v.id === id);
  if (va) {
    const nuevo_nombre = prompt("Editar nombre:", va.nombre);
    const nuevo_tipo = prompt("Editar tipo (vendedor/arrendatario):", va.tipo);
    const nueva_cedula = prompt("Editar cédula:", va.cedula);
    const nuevo_correo = prompt("Editar correo:", va.correo);
    const nuevo_telefono = prompt("Editar teléfono:", va.telefono);
    const nueva_direccion = prompt("Editar dirección:", va.direccion);
    const nuevo_estado = prompt("Editar estado (activo/inactivo/pendiente):", va.estado);
    const nuevas_observaciones = prompt("Editar observaciones:", va.observaciones || "");

    if (nuevo_nombre && nuevo_tipo && nueva_cedula && nuevo_correo) {
      const idx = vendedor_arrendatario.findIndex(v => v.id === id);
      if (idx !== -1) {
        vendedor_arrendatario[idx].nombre = nuevo_nombre;
        vendedor_arrendatario[idx].tipo = nuevo_tipo;
        vendedor_arrendatario[idx].cedula = nueva_cedula;
        vendedor_arrendatario[idx].correo = nuevo_correo;
        vendedor_arrendatario[idx].telefono = nuevo_telefono || va.telefono;
        vendedor_arrendatario[idx].direccion = nueva_direccion || va.direccion;
        vendedor_arrendatario[idx].estado = nuevo_estado || va.estado;
        vendedor_arrendatario[idx].observaciones = nuevas_observaciones || va.observaciones;
        vendedor_arrendatario[idx].iniciales = nuevo_nombre.split(" ").map(p => p[0]).join("").slice(0, 2);
        renderizar_vendedor_arrendatario();
      }
    }
  }
}

function eliminar_vendedor_arrendatario(id) {
  const va = vendedor_arrendatario.find(v => v.id === id);
  if (va && confirm(`¿Eliminar "${va.nombre}"? Esta acción no se puede deshacer.`)) {
    const idx = vendedor_arrendatario.findIndex(v => v.id === id);
    if (idx !== -1) {
      vendedor_arrendatario.splice(idx, 1);
      renderizar_vendedor_arrendatario();
    }
  }
}

function mostrar_configuracion() {
  alert('Herramientas de configuración (función en construcción)');
}
/* ──────────────────────────────────────────────────────
   10. MENÚ DE USUARIO (CERRAR SESIÓN)
   ────────────────────────────────────────────────────── */
function alternar_menu_usuario_admin() {
  estado.menu_usuario_admin_abierto = !estado.menu_usuario_admin_abierto;

  const menu = document.getElementById("menu_usuario_admin");
  const boton = document.querySelector(".barra_lateral__avatar--clickeable");

  if (menu) {
    menu.classList.toggle("menu_usuario_admin--oculto", !estado.menu_usuario_admin_abierto);
  }

  if (boton) {
    boton.setAttribute("aria-expanded", estado.menu_usuario_admin_abierto);
  }
}


function ir_a_perfil_admin() {
  cerrar_menu_usuario_admin();
  alert("Función de perfil del administrador (en construcción)");
}

function cerrar_sesion_admin() {
  localStorage.removeItem('sesion_activa');
  localStorage.removeItem('token');
  localStorage.removeItem('usuario_actual');

  estado.menu_usuario_admin_abierto = false;
  cerrar_menu_usuario_admin();

  alert("Sesión cerrada correctamente");

  setTimeout(() => {
    window.location.href = '../html/login.html';
  }, 500);
}

// Cerrar menú al hacer clic fuera
document.addEventListener('click', (e) => {
  const menu = document.getElementById("menu_usuario_admin");
  const boton = document.querySelector(".barra_lateral__avatar--clickeable");

  if (estado.menu_usuario_admin_abierto && menu && boton) {
    if (!menu.contains(e.target) && !boton.contains(e.target)) {
      cerrar_menu_usuario_admin();
    }
  }
});

/* ──────────────────────────────────────────────────────
   11. INICIALIZACIÓN
   ────────────────────────────────────────────────────── */
function inicializar_panel_admin() {
  // Items de la barra lateral
  document.querySelectorAll(".barra_lateral__item").forEach((item) => {
    if (!item.classList.contains("barra_lateral__item--inactivo")) {
      item.addEventListener("click", () => cambiar_vista_admin(item.dataset.vista));
    }
  });

  // Nav inferior móvil
  document.querySelectorAll(".nav_inferior_movil__item").forEach((item) => {
    item.addEventListener("click", () => cambiar_vista_admin(item.dataset.vista));
  });

  // Botón volver en detalle
  const btn_volver = document.getElementById("boton_volver");
  if (btn_volver) {
    btn_volver.addEventListener("click", volver_a_listado);
  }

  // Búsquedas
  const busq_inmuebles = document.getElementById("buscador_inmuebles");
  if (busq_inmuebles) busq_inmuebles.addEventListener("input", filtrar_inmuebles);

  const busq_usuarios = document.getElementById("buscador_usuarios");
  if (busq_usuarios) busq_usuarios.addEventListener("input", handle_filtrar_usuarios);

  const busq_vendedor_arrendatario = document.getElementById("buscador_vendedor_arrendatario");
  if (busq_vendedor_arrendatario) busq_vendedor_arrendatario.addEventListener("input", filtrar_vendedor_arrendatario);

  // Activar vista inicial
  cambiar_vista_admin("tablero");
}

// Arrancar cuando el DOM esté listo
if (document.readyState === "loading") {
  document.addEventListener("DOMContentLoaded", inicializar);
} else {
  inicializar();
}
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

    // 1. Convierte la respuesta en JSON para sacar el token
    const resultado = await response.json();

    // 2. Guardar el token en el almacenamiento local
    localStorage.setItem('sesion_activa', 'true');
    localStorage.setItem('token', resultado.token);

    // 3. datos del usuario usando el token guardado
    const usuario = obtener_usuario_actual();

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
