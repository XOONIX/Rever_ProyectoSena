/* ═══════════════════════════════════════════════════════
   REVER INMOBILIARIA — Pantalla Mis Inmuebles (Vendedor)
   Nomenclatura: Español + snake_case
   Autor: Rever Inmobiliaria © 2026
════════════════════════════════════════════════════════ */

'use strict';

/* ── Estado global ── */
const estado_mis_inmuebles = {
  inmuebles: [],
  filtro_actual: 'todos',
  usuario_actual: null,
};

const API_URL = 'https://localhost:7015/api';

/* ── Utilidades ── */
function obtener_elemento(id) {
  return document.getElementById(id);
}

function mostrar_notificacion(mensaje, tipo) {
  const contenedor = obtener_elemento('contenedor_notificaciones');
  if (!contenedor) return;

  const notif = document.createElement('div');
  const colores = {
    exito: '#10B981',
    error: '#EF4444',
    info: '#3B82F6',
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

/* ── Navegación ── */
function volver_a_inicio() {
  window.location.href = 'index.html';
}

function ir_a_publicar() {
  window.location.href = 'publicar_inmueble.html';
}

function ver_detalle_inmueble(id) {
  window.location.href = `pantalla_detalle.html?id=${id}`;
}

/* ── Cargar inmuebles del vendedor ── */
async function cargar_mis_inmuebles() {
  const usuario = obtener_usuario_actual();
  
  if (!usuario) {
    mostrar_notificacion('Debes iniciar sesión para ver tus inmuebles', 'error');
    window.location.href = 'login.html';
    return;
  }

  estado_mis_inmuebles.usuario_actual = usuario;

  const estado_carga = obtener_elemento('estado_carga');
  const grid_inmuebles = obtener_elemento('grid_inmuebles');
  const estado_vacio = obtener_elemento('estado_vacio');

  if (estado_carga) estado_carga.classList.remove('oculto');
  if (grid_inmuebles) grid_inmuebles.classList.add('oculto');
  if (estado_vacio) estado_vacio.classList.add('oculto');

  try {
    const respuesta = await fetch(`${API_URL}/Inmueble/Usuario/${usuario.idUsuario}`);
    
    if (!respuesta.ok) {
      throw new Error('No se pudieron cargar tus inmuebles');
    }

    const datos = await respuesta.json();
    estado_mis_inmuebles.inmuebles = datos;

    actualizar_estadisticas();
    renderizar_inmuebles();

  } catch (error) {
    console.error('Error al cargar inmuebles:', error);
    mostrar_notificacion('No se pudieron cargar tus inmuebles', 'error');
    
    // Mostrar estado vacío en caso de error
    if (estado_carga) estado_carga.classList.add('oculto');
    if (estado_vacio) estado_vacio.classList.remove('oculto');
  } finally {
    if (estado_carga) estado_carga.classList.add('oculto');
  }
}

/* ── Actualizar estadísticas ── */
function actualizar_estadisticas() {
  const inmuebles = estado_mis_inmuebles.inmuebles;
  
  const total = inmuebles.length;
  const activos = inmuebles.filter(i => i.estado === 'verificada' || i.estado === 'activo').length;
  const pendientes = inmuebles.filter(i => i.estado === 'pendiente').length;
  const inactivos = inmuebles.filter(i => i.estado === 'inactivo' || i.estado === 'rechazada').length;

  const el_total = obtener_elemento('total_inmuebles');
  const el_activos = obtener_elemento('inmuebles_activos');
  const el_pendientes = obtener_elemento('inmuebles_pendientes');
  const el_inactivos = obtener_elemento('inmuebles_inactivos');

  if (el_total) el_total.textContent = total;
  if (el_activos) el_activos.textContent = activos;
  if (el_pendientes) el_pendientes.textContent = pendientes;
  if (el_inactivos) el_inactivos.textContent = inactivos;
}

/* ── Aplicar filtro ── */
function aplicar_filtro(filtro, boton) {
  estado_mis_inmuebles.filtro_actual = filtro;

  // Actualizar botones
  document.querySelectorAll('.filtro, .filtro_activo').forEach(btn => {
    btn.classList.remove('filtro_activo');
    btn.classList.add('filtro');
  });
  
  boton.classList.remove('filtro');
  boton.classList.add('filtro_activo');

  renderizar_inmuebles();
}

/* ── Renderizar inmuebles ── */
function renderizar_inmuebles() {
  const grid = obtener_elemento('grid_inmuebles');
  const estado_vacio = obtener_elemento('estado_vacio');
  
  if (!grid) return;

  let inmuebles_filtrados = estado_mis_inmuebles.inmuebles;

  // Aplicar filtro
  if (estado_mis_inmuebles.filtro_actual !== 'todos') {
    const filtro = estado_mis_inmuebles.filtro_actual;
    inmuebles_filtrados = inmuebles_filtrados.filter(inmueble => {
      const estado = inmueble.estado?.toLowerCase() || '';
      if (filtro === 'activo') {
        return estado === 'verificada' || estado === 'activo';
      } else if (filtro === 'pendiente') {
        return estado === 'pendiente';
      } else if (filtro === 'inactivo') {
        return estado === 'inactivo' || estado === 'rechazada';
      }
      return true;
    });
  }

  // Mostrar estado vacío si no hay inmuebles
  if (inmuebles_filtrados.length === 0) {
    grid.classList.add('oculto');
    if (estado_vacio) estado_vacio.classList.remove('oculto');
    return;
  }

  grid.classList.remove('oculto');
  if (estado_vacio) estado_vacio.classList.add('oculto');

  // Renderizar tarjetas
  grid.innerHTML = inmuebles_filtrados.map(inmueble => {
    const estado = inmueble.estado?.toLowerCase() || 'pendiente';
    let badge_clase = 'tarjeta_inmueble__badge--pendiente';
    let badge_texto = 'Pendiente';

    if (estado === 'verificada' || estado === 'activo') {
      badge_clase = 'tarjeta_inmueble__badge--activo';
      badge_texto = 'Activo';
    } else if (estado === 'inactivo' || estado === 'rechazada') {
      badge_clase = 'tarjeta_inmueble__badge--inactivo';
      badge_texto = 'Inactivo';
    }

    const imagen = inmueble.imagenUrl || inmueble.imagen || 'https://via.placeholder.com/400x300?text=Sin+imagen';
    const precio = inmueble.precio ? `$${inmueble.precio.toLocaleString('es-CO')}` : 'Precio no disponible';
    const ubicacion = inmueble.ubicacion || inmueble.ciudad || 'Ubicación no disponible';
    const habitaciones = inmueble.habitaciones || 0;
    const banos = inmueble.banos || 0;
    const area = inmueble.metrosCuadrados || inmueble.area || 0;

    return `
      <article class="tarjeta_inmueble" onclick="ver_detalle_inmueble(${inmueble.idInmueble || inmueble.id})">
        <div class="tarjeta_inmueble__imagen">
          <img src="${imagen}" alt="${inmueble.titulo}" loading="lazy" />
          <span class="tarjeta_inmueble__badge ${badge_clase}">${badge_texto}</span>
          <div class="tarjeta_inmueble__acciones">
            <button class="tarjeta_inmueble__accion" onclick="event.stopPropagation(); editar_inmueble(${inmueble.idInmueble || inmueble.id})" title="Editar">
              <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                <path d="M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7"/>
                <path d="M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z"/>
              </svg>
            </button>
            <button class="tarjeta_inmueble__accion" onclick="event.stopPropagation(); eliminar_inmueble(${inmueble.idInmueble || inmueble.id})" title="Eliminar">
              <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                <polyline points="3 6 5 6 21 6"/>
                <path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"/>
              </svg>
            </button>
          </div>
        </div>
        <div class="tarjeta_inmueble__cuerpo">
          <h3 class="tarjeta_inmueble__titulo">${inmueble.titulo}</h3>
          <p class="tarjeta_inmueble__ubicacion">
            <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
              <path d="M21 10c0 7-9 13-9 13s-9-6-9-13a9 9 0 0 1 18 0z"/>
              <circle cx="12" cy="10" r="3"/>
            </svg>
            ${ubicacion}
          </p>
          <p class="tarjeta_inmueble__precio">${precio}</p>
          <div class="tarjeta_inmueble__info">
            <div class="tarjeta_inmueble__info_item">
              <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                <path d="M3 9l9-7 9 7v11a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2z"/>
              </svg>
              ${habitaciones}
            </div>
            <div class="tarjeta_inmueble__info_item">
              <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                <path d="M9.59 4.59A2 2 0 1 1 11 8H2m10.59 11.41A2 2 0 1 0 14 10H2m15.73-8.27A2.5 2.5 0 1 1 19.5 4H2"/>
              </svg>
              ${banos}
            </div>
            <div class="tarjeta_inmueble__info_item">
              <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                <rect x="3" y="3" width="18" height="18" rx="2" ry="2"/>
              </svg>
              ${area} m²
            </div>
          </div>
        </div>
      </article>
    `;
  }).join('');
}

/* ── Editar inmueble ── */
function editar_inmueble(id) {
  window.location.href = `vendedor_detalle_inmueble.html?id=${id}`;
}

/* ── Eliminar inmueble ── */
async function eliminar_inmueble(id) {
  if (!confirm('¿Estás seguro de que deseas eliminar este inmueble? Esta acción no se puede deshacer.')) {
    return;
  }

  const token = localStorage.getItem('token');

  try {
    const respuesta = await fetch(`${API_URL}/Inmueble/${id}`, {
      method: 'DELETE',
      headers: {
        'Authorization': `Bearer ${token}`,
      },
    });

    if (!respuesta.ok) {
      throw new Error('No se pudo eliminar el inmueble');
    }

    mostrar_notificacion('Inmueble eliminado correctamente', 'exito');
    cargar_mis_inmuebles(); // Recargar la lista

  } catch (error) {
    console.error('Error al eliminar inmueble:', error);
    mostrar_notificacion('No se pudo eliminar el inmueble', 'error');
  }
}

/* ── Inicialización ── */
function inicializar_mis_inmuebles() {
  cargar_mis_inmuebles();
}

document.addEventListener('DOMContentLoaded', inicializar_mis_inmuebles);
