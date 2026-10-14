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