/* ═══════════════════════════════════════════════════════════════
   REVER INMOBILIARIA — JavaScript Principal
   Nomenclatura: Español + snake_case
   Autor: Rever Inmobiliaria © 2026
═══════════════════════════════════════════════════════════════ */

'use strict';

/* ── Constantes ── */
const RETRASO_ANIMACION  = 1500;
const TIEMPO_NOTIFICACION = 3000;

/* ── Estado global de la aplicación ── */
const estado = {
  sesion_activa: false,
  nombre_usuario: null,
  email_usuario:  null,
  imagen_usuario: null,
  modo_navegacion: 'todos',   /* 'todos' | 'arriendo' | 'compra' */
  sidebar_abierto: true,
  menu_usuario_abierto: false,
  filtros: {
    tipos:    new Set(),
    precio_max: 70,
    habitaciones: null,
    banos:    null,
    parqueaderos: null,
    mascotas: false,
  },
  propiedades: [],
};

/* ── Base de datos de propiedades ── */
const datos_propiedades = [
  {
    id: 1,
    titulo:     'Apartamento Moderno Chapinero',
    precio_etiqueta: '$1.200.000/mes',
    precio:     12,
    ubicacion:  'Chapinero, Bogotá',
    hab:        2, banos: 2, parqueaderos: 1, area: 65,
    imagen: 'https://images.unsplash.com/photo-1512917774080-9991f1c4c750?w=800&q=80',
    badge:      'Nuevo',
    modo:       'arriendo',
    tipo:       'Apartamentos',
    mascotas:   true,
  },
  {
    id: 2,
    titulo:     'Casa Familiar Usaquén',
    precio_etiqueta: '$850.000.000',
    precio:     85,
    ubicacion:  'Usaquén, Bogotá',
    hab:        4, banos: 3, parqueaderos: 2, area: 220,
    imagen: 'https://images.unsplash.com/photo-1600596542815-ffad4c1539a9?w=800&q=80',
    badge:      'Destacado',
    modo:       'compra',
    tipo:       'Casas',
    mascotas:   false,
  },
  {
    id: 3,
    titulo:     'Estudio Ejecutivo La Candelaria',
    precio_etiqueta: '$750.000/mes',
    precio:     7,
    ubicacion:  'La Candelaria, Bogotá',
    hab:        1, banos: 1, parqueaderos: 0, area: 38,
    imagen: 'https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?w=800&q=80',
    badge:      'Popular',
    modo:       'arriendo',
    tipo:       'Estudios',
    mascotas:   true,
  },
  {
    id: 4,
    titulo:     'Finca Campestre Sopó',
    precio_etiqueta: '$1.200.000.000',
    precio:     100,
    ubicacion:  'Sopó, Cundinamarca',
    hab:        5, banos: 4, parqueaderos: 4, area: 850,
    imagen: 'https://images.unsplash.com/photo-1564013799919-ab600027ffc6?w=800&q=80',
    badge:      'Premium',
    modo:       'compra',
    tipo:       'Fincas',
    mascotas:   false,
  },
  {
    id: 5,
    titulo:     'Oficina Corporativa El Nogal',
    precio_etiqueta: '$4.500.000/mes',
    precio:     45,
    ubicacion:  'El Nogal, Bogotá',
    hab:        0, banos: 2, parqueaderos: 3, area: 120,
    imagen: 'https://images.unsplash.com/photo-1497366216548-37526070297c?w=800&q=80',
    badge:      'Disponible',
    modo:       'arriendo',
    tipo:       'Oficinas',
    mascotas:   false,
  },
  {
    id: 6,
    titulo:     'Apartamento Vista Mar Cartagena',
    precio_etiqueta: '$450.000.000',
    precio:     45,
    ubicacion:  'Bocagrande, Cartagena',
    hab:        3, banos: 2, parqueaderos: 1, area: 95,
    imagen: 'https://images.unsplash.com/photo-1600585154340-be6161a56a0c?w=800&q=80',
    badge:      'Único',
    modo:       'compra',
    tipo:       'Apartamentos',
    mascotas:   true,
  },
  {
    id: 7,
    titulo:     'Casa Moderna Laureles Medellín',
    precio_etiqueta: '$2.800.000/mes',
    precio:     28,
    ubicacion:  'Laureles, Medellín',
    hab:        3, banos: 2, parqueaderos: 1, area: 140,
    imagen: 'https://images.unsplash.com/photo-1568605114967-8130f3a36994?w=800&q=80',
    badge:      'Estreno',
    modo:       'arriendo',
    tipo:       'Casas',
    mascotas:   true,
  },
  {
    id: 8,
    titulo:     'Penthouse Exclusivo Poblado',
    precio_etiqueta: '$2.100.000.000',
    precio:     98,
    ubicacion:  'El Poblado, Medellín',
    hab:        4, banos: 5, parqueaderos: 3, area: 380,
    imagen: 'https://images.unsplash.com/photo-1613977257363-707ba9348227?w=800&q=80',
    badge:      'Lujo',
    modo:       'compra',
    tipo:       'Apartamentos',
    mascotas:   false,
  },
  {
    id: 9,
    titulo:     'Local Comercial Zona Rosa',
    precio_etiqueta: '$6.500.000/mes',
    precio:     65,
    ubicacion:  'Zona Rosa, Bogotá',
    hab:        0, banos: 1, parqueaderos: 0, area: 80,
    imagen: 'https://images.unsplash.com/photo-1604328698692-f76ea9498e76?w=800&q=80',
    badge:      'Estratégico',
    modo:       'arriendo',
    tipo:       'Oficinas',
    mascotas:   false,
  },
];

estado.propiedades = datos_propiedades;

/* ════════════════════════════════════════════════════════════
   UTILIDADES
════════════════════════════════════════════════════════════ */

/**
 * Obtiene un elemento por ID.
 * @param {string} id
 * @returns {HTMLElement|null}
 */
function obtener_elemento(id) {
  return document.getElementById(id);
}

/**
 * Formatea el precio según el valor del deslizador (0-100).
 * @param {number} valor - 0 a 100
 * @returns {string} precio formateado
 */
function formatear_precio(valor) {
  if (valor >= 100) return '$1.000M+';
  const precio = Math.round(100 + valor * 9);
  if (precio >= 1000) return '$' + (precio / 1000).toFixed(precio % 1000 === 0 ? 0 : 1) + 'B';
  return '$' + precio + 'M';
}

/**
 * Muestra u oculta el spinner de carga dentro de un botón.
 * @param {HTMLButtonElement} boton
 * @param {boolean} cargando
 * @param {string} texto_normal
 */
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

/**
 * Muestra una notificación temporal en pantalla.
 * @param {string} mensaje
 * @param {'exito'|'error'|'info'} tipo
 */
function mostrar_notificacion(mensaje, tipo) {
  const contenedor = obtener_elemento('contenedor_notificaciones');
  if (!contenedor) return;

  const notif = document.createElement('div');
  notif.setAttribute('role', 'alert');
  notif.setAttribute('aria-live', 'polite');

  const colores = {
    exito: 'background:#10B981;color:#fff',
    error: 'background:#EF4444;color:#fff',
    info:  'background:#3B82F6;color:#fff',
  };

  notif.setAttribute('style',
    'padding:.75rem 1.25rem;border-radius:.5rem;font-size:.875rem;font-weight:600;' +
    'box-shadow:0 4px 12px rgba(0,0,0,.15);' + (colores[tipo] || colores.info) +
    ';animation:aparecer_notif .3s ease;max-width:20rem;'
  );
  notif.textContent = mensaje;
  contenedor.appendChild(notif);

  setTimeout(() => {
    notif.style.animation = 'desaparecer_notif .3s ease forwards';
    setTimeout(() => notif.remove(), 300);
  }, TIEMPO_NOTIFICACION);
}

/**
 * Aplica animación de sacudida a un elemento.
 * @param {HTMLElement} elemento
 */
function animar_sacudir(elemento) {
  elemento.classList.add('animacion_sacudir');
  elemento.addEventListener('animationend', () => {
    elemento.classList.remove('animacion_sacudir');
  }, { once: true });
}


/* ════════════════════════════════════════════════════════════
   PANEL PRINCIPAL
════════════════════════════════════════════════════════════ */

/**
 * Muestra el panel principal del dashboard.
 */
function mostrar_panel_principal() {
  const panel = obtener_elemento('panel_principal');
  if (panel) {
    panel.classList.remove('oculto');
  }
}


/* ════════════════════════════════════════════════════════════
   NAVEGACIÓN Y MODO
════════════════════════════════════════════════════════════ */

/**
 * Cambia el modo de navegación (todos / arriendo / compra).
 * @param {string} modo - '' | 'arriendo' | 'compra'
 */
function cambiar_modo_navegacion(modo) {
  estado.modo_navegacion = modo === '' ? 'todos' : modo;

  /* Actualiza clases activo en botones nav */
  document.querySelectorAll('[data-modo]').forEach(boton => {
    const modo_boton = boton.dataset.modo;
    boton.classList.toggle('activo', modo_boton === modo);
  });

  /* Actualiza título de sección */
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

  /* Si se navega al inicio (modo vacío), limpiar filtros */
  if (modo === '') {
    limpiar_filtros();
  }

  /* Cierra menú móvil si está abierto */
  cerrar_menu_movil();
  renderizar_propiedades();
}

/**
 * Alterna la visibilidad del menú móvil.
 */
function alternar_menu_movil() {
  const menu = obtener_elemento('menu_movil');
  if (!menu) return;
  const visible = !menu.classList.contains('oculto');
  menu.classList.toggle('oculto', visible);
}

/**
 * Cierra el menú móvil.
 */
function cerrar_menu_movil() {
  const menu = obtener_elemento('menu_movil');
  if (menu) menu.classList.add('oculto');
}

/**
 * Muestra la sección de favoritos.
 */
function mostrar_favoritos() {
  mostrar_notificacion('Sección de favoritos (función en construcción)', 'info');
}

/**
 * Muestra el mapa de propiedades.
 */
function mostrar_mapa() {
  mostrar_notificacion('Mapa de propiedades (función en construcción)', 'info');
}

/**
 * Alterna el menú desplegable de usuario.
 */
function alternar_menu_usuario() {
  estado.menu_usuario_abierto = !estado.menu_usuario_abierto;
  
  const menu = obtener_elemento('menu_usuario');
  const boton = document.querySelector('.boton_avatar_usuario');
  
  if (menu) {
    menu.classList.toggle('menu_usuario--oculto', !estado.menu_usuario_abierto);
  }
  
  if (boton) {
    boton.setAttribute('aria-expanded', estado.menu_usuario_abierto);
  }
}

/**
 * Cierra el menú de usuario si está abierto.
 */
function cerrar_menu_usuario() {
  if (estado.menu_usuario_abierto) {
    estado.menu_usuario_abierto = false;
    
    const menu = obtener_elemento('menu_usuario');
    const boton = document.querySelector('.boton_avatar_usuario');
    
    if (menu) {
      menu.classList.add('menu_usuario--oculto');
    }
    
    if (boton) {
      boton.setAttribute('aria-expanded', 'false');
    }
  }
}

/**
 * Verifica el estado de autenticación del usuario.
 */
function verificar_autenticacion() {
  // Verificar si hay un usuario en localStorage
  const usuarioGuardado = localStorage.getItem('usuario_actual');
  const sesionGuardada = localStorage.getItem('sesion_activa');
  
  estado.sesion_activa = sesionGuardada === 'true' && usuarioGuardado;
  
  if (estado.sesion_activa) {
    const usuario = JSON.parse(usuarioGuardado);
    estado.nombre_usuario = usuario.nombre;
    estado.email_usuario = usuario.correo;
    estado.imagen_usuario = usuario.imagen_perfil || null;
    
    // Actualizar avatar si hay imagen personalizada
    if (estado.imagen_usuario) {
      const avatarImagen = document.getElementById('avatar_imagen');
      if (avatarImagen) {
        avatarImagen.src = estado.imagen_usuario;
      }
    }
    
    mostrar_opciones_autenticado();
  } else {
    mostrar_opciones_no_autenticado();
  }
}

/**
 * Muestra las opciones para usuario autenticado.
 */
function mostrar_opciones_autenticado() {
  const opcionesAuth = obtener_elemento('opciones_autenticado');
  const opcionesNoAuth = obtener_elemento('opciones_no_autenticado');
  
  if (opcionesAuth) opcionesAuth.classList.remove('oculto');
  if (opcionesNoAuth) opcionesNoAuth.classList.add('oculto');
}

/**
 * Muestra las opciones para usuario no autenticado.
 */
function mostrar_opciones_no_autenticado() {
  const opcionesAuth = obtener_elemento('opciones_autenticado');
  const opcionesNoAuth = obtener_elemento('opciones_no_autenticado');
  
  if (opcionesAuth) opcionesAuth.classList.add('oculto');
  if (opcionesNoAuth) opcionesNoAuth.classList.remove('oculto');
}

/**
 * Navega a la página de perfil.
 */
function ir_a_perfil() {
  cerrar_menu_usuario();
  window.location.href = 'perfil.html';
}

/**
 * Navega a la sección de favoritos.
 */
function ir_a_favoritos() {
  cerrar_menu_usuario();
  mostrar_notificacion('Sección de favoritos (función en construcción)', 'info');
}

/**
 * Navega a la sección de publicaciones.
 */
function ir_a_publicaciones() {
  cerrar_menu_usuario();
  mostrar_notificacion('Sección de publicaciones (función en construcción)', 'info');
}

/**
 * Navega a la página de login.
 */
function ir_a_login() {
  cerrar_menu_usuario();
  window.location.href = 'login.html';
}

/**
 * Navega a la página de registro.
 */
function ir_a_registro() {
  cerrar_menu_usuario();
  window.location.href = 'registro.html';
}

/**
 * Cierra la sesión del usuario.
 */
function cerrar_sesion() {
  localStorage.removeItem('sesion_activa');
  localStorage.removeItem('usuario_actual');
  localStorage.removeItem('imagen_avatar');
  
  estado.sesion_activa = false;
  estado.nombre_usuario = null;
  estado.email_usuario = null;
  estado.imagen_usuario = null;
  
  // Restaurar avatar por defecto
  const avatarImagen = document.getElementById('avatar_imagen');
  if (avatarImagen) {
    avatarImagen.src = 'https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?ixlib=rb-1.2.1&auto=format&fit=facearea&facepad=2&w=256&h=256&q=80';
  }
  
  mostrar_opciones_no_autenticado();
  cerrar_menu_usuario();
  
  mostrar_notificacion('Sesión cerrada correctamente', 'exito');
}


/* ════════════════════════════════════════════════════════════
   FILTROS
════════════════════════════════════════════════════════════ */

/**
 * Alterna el panel lateral de filtros.
 */
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

/**
 * Actualiza el estado visual de los botones de filtros (mostrar/ocultar botón limpiar).
 */
function actualizar_estado_botones_filtros() {
  const boton_limpiar = obtener_elemento('boton_limpiar');
  if (!boton_limpiar) return;

  const hay_filtros_activos =
    estado.filtros.tipos.size > 0 ||
    estado.filtros.precio_max !== 70 ||
    estado.filtros.habitaciones !== null ||
    estado.filtros.banos !== null ||
    estado.filtros.estacionamientos !== null ||
    estado.filtros.mascotas;

  boton_limpiar.classList.toggle('oculto', !hay_filtros_activos);
}

/**
 * Maneja el cambio de un checkbox de tipo de propiedad.
 * @param {HTMLInputElement} checkbox
 * @param {string} tipo
 */
function manejar_checkbox_tipo(checkbox, tipo) {
  if (checkbox.checked) {
    estado.filtros.tipos.add(tipo);
  } else {
    estado.filtros.tipos.delete(tipo);
  }
  actualizar_estado_botones_filtros();
  renderizar_propiedades();
}

/**
 * Actualiza el filtro de precio máximo al mover el deslizador.
 * @param {HTMLInputElement} deslizador
 */
function actualizar_precio(deslizador) {
  const valor = parseInt(deslizador.value, 10);
  estado.filtros.precio_max = valor;

  const etiqueta = obtener_elemento('valor_precio_actual');
  if (etiqueta) etiqueta.textContent = formatear_precio(valor);

  const relleno = obtener_elemento('relleno_deslizador');
  if (relleno) relleno.style.width = valor + '%';

  renderizar_propiedades();
}

/**
 * Selecciona o deselecciona una pastilla de filtro.
 * @param {HTMLElement} pastilla_elemento
 */
function seleccionar_pastilla(pastilla_elemento) {
  const grupo = pastilla_elemento.dataset.grupo;
  const valor = pastilla_elemento.dataset.valor;
  const ya_activa = pastilla_elemento.classList.contains('activa');

  /* Desactiva todas las pastillas del mismo grupo */
  const grupo_pastillas = pastilla_elemento.closest('.grupo_pastillas');
  if (grupo_pastillas) {
    grupo_pastillas.querySelectorAll('.pastilla').forEach(p => p.classList.remove('activa'));
  }

  if (ya_activa) {
    /* Deselecciona si ya estaba activa */
    estado.filtros[grupo] = null;
  } else {
    pastilla_elemento.classList.add('activa');
    /* Convertir valor a número si es posible, manejar caso especial de "4+" y "3+" */
    if (valor.includes('+')) {
      estado.filtros[grupo] = parseInt(valor.replace('+', ''), 10);
    } else {
      estado.filtros[grupo] = parseInt(valor, 10);
    }
  }

  actualizar_estado_botones_filtros();
  renderizar_propiedades();
}

/**
 * Alterna el filtro de mascotas permitidas.
 * @param {HTMLInputElement} checkbox
 */
function alternar_mascotas(checkbox) {
  estado.filtros.mascotas = checkbox.checked;
  actualizar_estado_botones_filtros();
  renderizar_propiedades();
}

/**
 * Limpia todos los filtros activos.
 */
function limpiar_filtros() {
  estado.filtros.tipos.clear();
  estado.filtros.precio_max = 70;
  estado.filtros.habitaciones = null;
  estado.filtros.banos = null;
  estado.filtros.estacionamientos = null;
  estado.filtros.mascotas = false;

  /* Resetea checkboxes */
  document.querySelectorAll('.etiqueta_checkbox input[type="checkbox"]').forEach(cb => {
    if (cb.id !== 'checkbox_pet_friendly') cb.checked = false;
  });

  /* Resetea mascota */
  const cb_mascotas = obtener_elemento('checkbox_pet_friendly');
  if (cb_mascotas) cb_mascotas.checked = false;

  /* Resetea pastillas */
  document.querySelectorAll('.pastilla').forEach(p => p.classList.remove('activa'));

  /* Resetea deslizador */
  const deslizador = obtener_elemento('deslizador_precio');
  if (deslizador) deslizador.value = 70;
  const etiqueta = obtener_elemento('valor_precio_actual');
  if (etiqueta) etiqueta.textContent = formatear_precio(70);
  const relleno = obtener_elemento('relleno_deslizador');
  if (relleno) relleno.style.width = '70%';

  actualizar_estado_botones_filtros();
  renderizar_propiedades();
}


/* ════════════════════════════════════════════════════════════
   RENDERIZADO DE PROPIEDADES
════════════════════════════════════════════════════════════ */

/**
 * Filtra las propiedades según el estado actual.
 * @returns {Array} propiedades filtradas
 */
function obtener_propiedades_filtradas() {
  return estado.propiedades.filter(prop => {
    /* Filtro de modo */
    if (estado.modo_navegacion !== 'todos' && prop.modo !== estado.modo_navegacion) return false;

    /* Filtro de tipo */
    if (estado.filtros.tipos.size > 0 && !estado.filtros.tipos.has(prop.tipo)) return false;

    /* Filtro de precio */
    if (prop.precio > estado.filtros.precio_max) return false;

    /* Filtro habitaciones */
    if (estado.filtros.habitaciones !== null && prop.hab < estado.filtros.habitaciones) return false;

    /* Filtro baños */
    if (estado.filtros.banos !== null && prop.banos < estado.filtros.banos) return false;

    /* Filtro parqueadero */
    if (estado.filtros.estacionamientos !== null && prop.parqueaderos < estado.filtros.estacionamientos) return false;

    /* Filtro mascotas */
    if (estado.filtros.mascotas && !prop.mascotas) return false;

    return true;
  });
}

/**
 * Genera el HTML de una tarjeta de propiedad.
 * @param {Object} prop
 * @returns {string} HTML string
 */
function crear_html_tarjeta(prop) {
  const especificaciones = [];
  if (prop.hab > 0)  especificaciones.push(`${prop.hab} hab`);
  if (prop.banos > 0) especificaciones.push(`${prop.banos} baños`);
  if (prop.parqueaderos > 0) especificaciones.push(`${prop.parqueaderos} parq.`);
  especificaciones.push(`${prop.area}m²`);

  const espec_html = especificaciones
    .map((e, i) => i < especificaciones.length - 1
      ? `${e} <span class="separador_spec">·</span>`
      : e)
    .join(' ');

  const icono_mascota = prop.mascotas
    ? '<span class="icono_pet" title="Mascotas permitidas">🐾</span>'
    : '';

  return `
    <article class="tarjeta_propiedad" tabindex="0" aria-label="${prop.titulo}">
      <div class="contenedor_imagen_propiedad">
        <img
          src="${prop.imagen}"
          alt="${prop.titulo}"
          class="imagen_propiedad"
          loading="lazy"
        />
        <div class="degradado_imagen_propiedad" aria-hidden="true"></div>
        ${prop.badge ? `<span class="badge_propiedad">${prop.badge}</span>` : ''}
        <span class="badge_modo ${prop.modo}" aria-label="Modo: ${prop.modo}">
          ${prop.modo.charAt(0).toUpperCase() + prop.modo.slice(1)}
        </span>
        <p class="precio_propiedad">${prop.precio_etiqueta}</p>
        <button
          class="boton_favorito"
          aria-label="Agregar a favoritos: ${prop.titulo}"
          onclick="alternar_favorito(this, ${prop.id})"
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
          ${prop.ubicacion}
        </p>
        <p class="especificaciones_propiedad" aria-label="Especificaciones: ${especificaciones.join(', ')}">
          ${espec_html} ${icono_mascota}
        </p>
      </div>
    </article>
  `;
}

/**
 * Renderiza la grilla de propiedades filtradas en el DOM.
 */
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

/**
 * Alterna el estado de favorito de una tarjeta (efecto visual).
 * @param {HTMLButtonElement} boton
 * @param {number} id_propiedad
 */
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


/* ════════════════════════════════════════════════════════════
   INICIALIZACIÓN
════════════════════════════════════════════════════════════ */

/**
 * Inicializa todos los event listeners de la aplicación.
 */
function inicializar_app() {
  /* ── Tecla Escape para cerrar menús ── */
  document.addEventListener('keydown', evento => {
    if (evento.key === 'Escape') {
      cerrar_menu_movil();
    }
  });

  /* ── Accesibilidad: Enter en tarjetas ── */
  document.addEventListener('keydown', evento => {
    if (evento.key === 'Enter' && evento.target.classList.contains('tarjeta_propiedad')) {
      mostrar_notificacion('Ver detalle de propiedad (función en construcción)', 'info');
    }
  });

  /* Renderizar propiedades iniciales */
  renderizar_propiedades();
  
  /* Verificar estado de autenticación */
  verificar_autenticacion();
  
  /* Cerrar menú de usuario al hacer clic fuera */
  document.addEventListener('click', function(evento) {
    const contenedorMenu = document.querySelector('.contenedor_menu_usuario');
    if (contenedorMenu && !contenedorMenu.contains(evento.target)) {
      cerrar_menu_usuario();
    }
  });
}

/* Ejecutar cuando el DOM esté listo */
document.addEventListener('DOMContentLoaded', inicializar_app);
