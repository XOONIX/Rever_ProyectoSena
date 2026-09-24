/* ═══════════════════════════════════════════════════════════════
   REVER INMOBILIARIA — Pantalla de Detalle de Propiedad
   Nomenclatura: Español + snake_case
   Autor: Rever Inmobiliaria © 2026
═══════════════════════════════════════════════════════════════ */

'use strict';

/* ════════════════════════════════════════════════════════════
   BASE DE DATOS DE PROPIEDADES
════════════════════════════════════════════════════════════ */

/* Los datos se cargan desde el archivo compartido datos_propiedades.js */
const propiedades_db = datos_propiedades;


/* ════════════════════════════════════════════════════════════
   ESTADO GLOBAL
════════════════════════════════════════════════════════════ */

const estado_detalle = {
  propiedad_actual: null,   /* Objeto propiedad cargado */
  indice_galeria:   0,      /* Foto activa en el modal */
  es_favorito:      false,
};


/* ════════════════════════════════════════════════════════════
   UTILIDADES
════════════════════════════════════════════════════════════ */

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
function mostrar_notificacion_detalle(mensaje, tipo) {
  const contenedor = obtener_elemento_detalle('contenedor_notificaciones');
  if (!contenedor) return;

  const notif = document.createElement('div');
  const colores = {
    exito: '#10B981',
    error: '#EF4444',
    info:  '#3B82F6',
  };

  notif.setAttribute('role', 'alert');
  notif.setAttribute('aria-live', 'polite');
  notif.style.cssText =
    'padding:.75rem 1.25rem;border-radius:.5rem;font-size:.875rem;font-weight:600;' +
    'box-shadow:0 4px 12px rgba(0,0,0,.15);background:' + (colores[tipo] || colores.info) +
    ';color:#fff;animation:aparecer_notif .3s ease;max-width:20rem;font-family:inherit;';
  notif.textContent = mensaje;
  contenedor.appendChild(notif);

  setTimeout(() => {
    notif.style.animation = 'desaparecer_notif .3s ease forwards';
    setTimeout(() => notif.remove(), 300);
  }, 3000);
}

/**
 * Lee el parámetro `id` de la URL (?id=1).
 * @returns {number|null}
 */
function obtener_id_desde_url() {
  const params = new URLSearchParams(window.location.search);
  const id = parseInt(params.get('id') ?? '1', 10);
  return isNaN(id) ? 1 : id;
}


/* ════════════════════════════════════════════════════════════
   RENDERIZADO DEL DETALLE
════════════════════════════════════════════════════════════ */

/**
 * Monta toda la pantalla de detalle con la propiedad indicada.
 * @param {Object} prop
 */
function renderizar_detalle(prop) {
  estado_detalle.propiedad_actual = prop;

  /* ── Hero ── */
  const img_hero = obtener_elemento_detalle('imagen_hero');
  if (img_hero) {
    img_hero.src = prop.imagenes[0];
    img_hero.alt = prop.titulo;
    img_hero.onclick = () => abrir_galeria(0);
  }

  /* Badges del hero */
  const badge_modo = obtener_elemento_detalle('badge_modo_hero');
  if (badge_modo) {
    badge_modo.textContent = prop.modo.charAt(0).toUpperCase() + prop.modo.slice(1);
    badge_modo.className = 'badge_modo_hero ' + prop.modo;
  }

  const badge_cat = obtener_elemento_detalle('badge_categoria_hero');
  if (badge_cat) {
    if (prop.badge) {
      badge_cat.textContent = prop.badge;
      badge_cat.classList.remove('oculto');
    } else {
      badge_cat.classList.add('oculto');
    }
  }

  /* Botón ver galería */
  const texto_galeria = obtener_elemento_detalle('texto_ver_galeria');
  if (texto_galeria) {
    texto_galeria.textContent = `Ver todas las fotos (${prop.imagenes.length})`;
  }

  /* ── Miniaturas ── */
  renderizar_miniaturas(prop.imagenes);

  /* ── Título y dirección ── */
  const titulo = obtener_elemento_detalle('titulo_propiedad');
  if (titulo) titulo.textContent = prop.titulo;

  const texto_dir = obtener_elemento_detalle('texto_direccion');
  if (texto_dir) texto_dir.textContent = prop.direccion;

  const zona = obtener_elemento_detalle('zona_propiedad');
  if (zona) zona.textContent = prop.ubicacion;

  /* ── Chips de specs ── */
  renderizar_specs(prop);

  /* ── Descripción ── */
  const desc = obtener_elemento_detalle('texto_descripcion');
  if (desc) desc.textContent = prop.descripcion;

  /* ── Características ── */
  renderizar_caracteristicas(prop.caracteristicas);

  /* ── Galería en grilla ── */
  renderizar_grilla_galeria(prop.imagenes);

  /* ── Pin del mapa ── */
  const zona_pin = obtener_elemento_detalle('zona_pin_mapa');
  if (zona_pin) zona_pin.textContent = prop.ubicacion;

  /* ── Tarjeta contacto ── */
  renderizar_contacto(prop);

  /* Mensaje inicial del textarea */
  const textarea = obtener_elemento_detalle('input_mensaje_contacto');
  if (textarea) {
    textarea.value = `Hola, me interesa la propiedad "${prop.titulo}". ¿Podría brindarme más información?`;
  }
}

/**
 * Renderiza las miniaturas de fotos adicionales.
 * @param {string[]} imagenes
 */
function renderizar_miniaturas(imagenes) {
  const contenedor = obtener_elemento_detalle('lista_miniaturas');
  if (!contenedor) return;

  contenedor.innerHTML = imagenes.slice(1).map((img, i) => `
    <div class="miniatura_foto" role="listitem">
      <img
        src="${img}"
        alt="Vista ${i + 2} de la propiedad"
        loading="lazy"
        onclick="abrir_galeria(${i + 1})"
      />
    </div>
  `).join('');
}

/**
 * Renderiza los chips de especificaciones.
 * @param {Object} prop
 */
function renderizar_specs(prop) {
  const contenedor = obtener_elemento_detalle('grilla_specs');
  if (!contenedor) return;

  const specs = [];

  if (prop.hab > 0) {
    specs.push({
      icono: '🛏️',
      valor: prop.hab,
      etiqueta: 'Habitaciones',
    });
  }

  specs.push({
    icono: '🚿',
    valor: prop.banos,
    etiqueta: 'Baños',
  });

  if (prop.parqueaderos > 0) {
    specs.push({
      icono: '🚗',
      valor: prop.parqueaderos,
      etiqueta: 'Parqueaderos',
    });
  }

  specs.push({
    icono: '📐',
    valor: prop.area_str || prop.area + ' m²',
    etiqueta: 'Área',
  });

  specs.push({
    icono: '🏠',
    valor: prop.tipo,
    etiqueta: 'Tipo',
  });

  if (prop.piso && prop.piso > 1) {
    specs.push({
      icono: '🏢',
      valor: `${prop.piso}°`,
      etiqueta: 'Piso',
    });
  }

  if (prop.antiguedad !== undefined) {
    specs.push({
      icono: '📅',
      valor: prop.antiguedad === 0 ? 'Estreno' : `${prop.antiguedad} años`,
      etiqueta: 'Antigüedad',
    });
  }

  if (prop.mascotas) {
    specs.push({
      icono: '🐾',
      valor: 'Sí',
      etiqueta: 'Mascotas',
    });
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

  contenedor.innerHTML = caracteristicas.map(c => `
    <div class="chip_caracteristica" role="listitem">
      <span class="icono_check_caracteristica" aria-hidden="true"></span>
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
  if (precio) precio.textContent = prop.precio_etiqueta;

  const avatar = obtener_elemento_detalle('avatar_asesor');
  if (avatar) avatar.textContent = prop.asesor.inicial;

  const nombre = obtener_elemento_detalle('nombre_asesor');
  if (nombre) nombre.textContent = prop.asesor.nombre;

  const enlace_llamar = obtener_elemento_detalle('enlace_llamar');
  if (enlace_llamar) enlace_llamar.href = `tel:${prop.asesor.telefono}`;

  const enlace_email = obtener_elemento_detalle('enlace_email');
  if (enlace_email) enlace_email.href = `mailto:${prop.asesor.email}`;

  /* Subtexto del estado de éxito */
  const subtexto = obtener_elemento_detalle('subtexto_exito_contacto');
  if (subtexto) subtexto.textContent = `${prop.asesor.nombre} te contactará pronto.`;
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
    /* Foco para accesibilidad */
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

  const total = prop.imagenes.length;
  estado_detalle.indice_galeria = (estado_detalle.indice_galeria + direccion + total) % total;
  actualizar_imagen_galeria();
}

/**
 * Actualiza la imagen activa y el contador en el modal.
 */
function actualizar_imagen_galeria() {
  const prop = estado_detalle.propiedad_actual;
  if (!prop) return;

  const img = obtener_elemento_detalle('imagen_galeria_activa');
  const contador = obtener_elemento_detalle('contador_galeria');

  if (img) {
    img.classList.add('cambiando');
    img.src = prop.imagenes[estado_detalle.indice_galeria];
    img.alt = `Foto ${estado_detalle.indice_galeria + 1} de ${prop.titulo}`;
    img.addEventListener('animationend', () => img.classList.remove('cambiando'), { once: true });
  }

  if (contador) {
    contador.textContent = `${estado_detalle.indice_galeria + 1} / ${prop.imagenes.length}`;
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
    let favoritos = JSON.parse(localStorage.getItem('favoritos') || '[]');
    
    if (estado_detalle.es_favorito) {
      if (!favoritos.includes(prop.id)) {
        favoritos.push(prop.id);
      }
    } else {
      favoritos = favoritos.filter(id => id !== prop.id);
    }
    
    localStorage.setItem('favoritos', JSON.stringify(favoritos));
  }

  const mensaje = estado_detalle.es_favorito
    ? '❤️ Guardado en favoritos'
    : 'Removido de favoritos';
  mostrar_notificacion_detalle(mensaje, estado_detalle.es_favorito ? 'exito' : 'info');
}

/**
 * Simula compartir la propiedad (usa la API Web Share si está disponible).
 */
function compartir_propiedad() {
  const prop = estado_detalle.propiedad_actual;
  if (!prop) return;

  if (navigator.share) {
    navigator.share({
      title: prop.titulo,
      text: `Mira esta propiedad en Rever Inmobiliaria: ${prop.titulo} — ${prop.precio_etiqueta}`,
      url: window.location.href,
    }).catch(() => {});
  } else {
    /* Fallback: copiar URL */
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
 * Maneja el envío del formulario de contacto.
 * @param {Event} evento
 */
function manejar_envio_contacto(evento) {
  evento.preventDefault();

  const nombre  = obtener_elemento_detalle('input_nombre_contacto');
  const email   = obtener_elemento_detalle('input_email_contacto');
  const boton   = obtener_elemento_detalle('boton_enviar_contacto');
  const error_nombre = obtener_elemento_detalle('error_nombre');
  const error_email  = obtener_elemento_detalle('error_email');

  let valido = true;

  /* Validar nombre */
  if (!nombre?.value?.trim()) {
    error_nombre?.classList.remove('oculto');
    nombre?.classList.add('campo_invalido');
    valido = false;
  } else {
    error_nombre?.classList.add('oculto');
    nombre?.classList.remove('campo_invalido');
  }

  /* Validar email */
  const regex_email = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  if (!email?.value?.trim() || !regex_email.test(email.value)) {
    error_email?.classList.remove('oculto');
    email?.classList.add('campo_invalido');
    valido = false;
  } else {
    error_email?.classList.add('oculto');
    email?.classList.remove('campo_invalido');
  }

  if (!valido) return;

  /* Simular envío */
  const texto_original = boton?.textContent ?? 'Enviar mensaje';
  if (boton) {
    boton.disabled = true;
    boton.innerHTML =
      '<svg width="16" height="16" viewBox="0 0 16 16" fill="none" style="animation:girar_carga .8s linear infinite">' +
      '<circle cx="8" cy="8" r="6" stroke="rgba(255,255,255,.3)" stroke-width="2"/>' +
      '<path d="M14 8a6 6 0 0 0-6-6" stroke="white" stroke-width="2" stroke-linecap="round"/></svg> Enviando…';
  }

  setTimeout(() => {
    /* Mostrar estado de éxito */
    obtener_elemento_detalle('formulario_contacto')?.classList.add('oculto');
    obtener_elemento_detalle('estado_exito_contacto')?.classList.remove('oculto');

    if (boton) {
      boton.disabled = false;
      boton.textContent = texto_original;
    }
  }, 1600);
}

/**
 * Restablece el formulario de contacto al estado inicial.
 */
function restablecer_formulario() {
  const prop = estado_detalle.propiedad_actual;

  obtener_elemento_detalle('estado_exito_contacto')?.classList.add('oculto');
  obtener_elemento_detalle('formulario_contacto')?.classList.remove('oculto');

  const nombre  = obtener_elemento_detalle('input_nombre_contacto');
  const email   = obtener_elemento_detalle('input_email_contacto');
  const mensaje = obtener_elemento_detalle('input_mensaje_contacto');

  if (nombre)  nombre.value  = '';
  if (email)   email.value   = '';
  if (mensaje && prop) {
    mensaje.value = `Hola, me interesa la propiedad "${prop.titulo}". ¿Podría brindarme más información?`;
  }
}


/* ════════════════════════════════════════════════════════════
   INICIALIZACIÓN
════════════════════════════════════════════════════════════ */

/**
 * Punto de entrada principal de la pantalla de detalle.
 */
function inicializar_detalle() {
  /* Asegurar que la pantalla sea visible inmediatamente */
  const overlay = obtener_elemento_detalle('overlay_carga');
  const pantalla = obtener_elemento_detalle('pantalla_detalle');
  
  if (overlay) overlay.classList.add('oculto');
  if (pantalla) pantalla.classList.remove('oculto');

  /* Determinar qué propiedad mostrar */
  const id = obtener_id_desde_url();
  const propiedad = propiedades_db.find(p => p.id === id) ?? propiedades_db[0];

  /* Actualizar título del documento */
  document.title = `${propiedad.titulo} — Rever Inmobiliaria`;

  /* Montar contenido */
  renderizar_detalle(propiedad);

  /* ── Event Listeners globales ── */

  /* Cerrar galería con Escape */
  document.addEventListener('keydown', evento => {
    if (evento.key === 'Escape') {
      cerrar_galeria();
    }
    /* Navegación con flechas en galería */
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
  
  /* Restaurar estado de favoritos desde localStorage si existe */
  const favoritos_guardados = localStorage.getItem('favoritos');
  if (favoritos_guardados) {
    const favoritos = JSON.parse(favoritos_guardados);
    if (favoritos.includes(propiedad.id)) {
      estado_detalle.es_favorito = true;
      const boton = obtener_elemento_detalle('boton_favorito_hero');
      if (boton) {
        boton.setAttribute('aria-pressed', 'true');
        boton.classList.add('favorito_activo');
      }
    }
  }
}

/* Ejecutar al cargar el DOM */
document.addEventListener('DOMContentLoaded', inicializar_detalle);

/**
 * Función para volver al index restaurando el estado de filtros
 */
function volver_con_estado() {
  // Navegar al index
  window.location.href = 'index.html';
}
