/* ═══════════════════════════════════════════════════════════════
   REVER INMOBILIARIA — Pantalla de Detalle de Propiedad
   Nomenclatura: Español + snake_case
   Autor: Rever Inmobiliaria © 2026
═══════════════════════════════════════════════════════════════ */

'use strict';

/* ════════════════════════════════════════════════════════════
   BASE DE DATOS DE PROPIEDADES
════════════════════════════════════════════════════════════ */

const propiedades_db = [
  {
    id: 1,
    titulo:        'Casa Rústica Acogedora',
    precio_etiqueta: '$250.000.000',
    precio:        250,
    ubicacion:     'Bosque de Pinos, Norte',
    direccion:     'Cra 15 #120-45, Bosque de Pinos, Bogotá',
    hab:  3, banos: 2, parqueaderos: 1, area: '120 m²',
    piso: 1, antiguedad: 5,
    imagenes: [
      'https://images.unsplash.com/photo-1772563139470-9232b4e435c2?w=1080&q=80',
      'https://images.unsplash.com/photo-1560185127-6a89d4a0b4a8?w=800&q=80',
      'https://images.unsplash.com/photo-1583608205776-bfd35f0d9f83?w=800&q=80',
      'https://images.unsplash.com/photo-1502005229762-cf1b2da7c5d6?w=800&q=80',
      'https://images.unsplash.com/photo-1556909114-f6e7ad7d3136?w=800&q=80',
    ],
    badge:    'NUEVO',
    modo:     'compra',
    tipo:     'Casas',
    mascotas: false,
    descripcion: 'Hermosa casa rústica ubicada en el exclusivo sector de Bosque de Pinos. Cuenta con acabados en madera natural, techos altos y una terraza con vista panorámica al jardín. La distribución amplia y luminosa hace de este inmueble un refugio perfecto para familias que buscan tranquilidad sin alejarse de la ciudad. Cocina integral con isla central, sala doble y zona de ropas independiente.',
    caracteristicas: ['Terraza privada', 'Jardín frontal', 'Cocina integral', 'Cuarto de servicio', 'Portería 24h'],
    asesor: { nombre: 'Carlos Mendoza', telefono: '+57 312 456 7890', email: 'carlos@reverinmobiliaria.com', inicial: 'C' },
  },
  {
    id: 2,
    titulo:        'Apartamento Moderno',
    precio_etiqueta: '$1.800.000/mes',
    precio:        1800,
    ubicacion:     'La Candelaria, Centro',
    direccion:     'Cl 12 #3-78, La Candelaria, Bogotá',
    hab:  2, banos: 1, parqueaderos: 0, area: '75 m²',
    piso: 7, antiguedad: 3,
    imagenes: [
      'https://images.unsplash.com/photo-1763419161907-1e00b2f883c5?w=1080&q=80',
      'https://images.unsplash.com/photo-1615873968403-89e068629265?w=800&q=80',
      'https://images.unsplash.com/photo-1507089947368-19c1da9775ae?w=800&q=80',
      'https://images.unsplash.com/photo-1484154218962-a197022b5858?w=800&q=80',
      'https://images.unsplash.com/photo-1556909114-f6e7ad7d3136?w=800&q=80',
    ],
    badge:    'DESTACADO',
    modo:     'arriendo',
    tipo:     'Apartamentos',
    mascotas: true,
    descripcion: 'Moderno apartamento en el corazón de La Candelaria con diseño contemporáneo y acabados de primera calidad. Ventanas amplias que inundan los espacios de luz natural. A pocos metros de universidades, restaurantes y el centro histórico de Bogotá.',
    caracteristicas: ['Mascotas permitidas', 'Gym en el edificio', 'Ascensor', 'Zona de lavandería', 'Depósito'],
    asesor: { nombre: 'Sandra Gómez', telefono: '+57 300 987 6543', email: 'sandra@reverinmobiliaria.com', inicial: 'S' },
  },
  {
    id: 3,
    titulo:        'Hogar Familiar Clásico',
    precio_etiqueta: '$320.000.000',
    precio:        320,
    ubicacion:     'Santa Bárbara, Norte',
    direccion:     'Cra 19 #127-65, Santa Bárbara, Bogotá',
    hab:  4, banos: 3, parqueaderos: 2, area: '200 m²',
    piso: 1, antiguedad: 12,
    imagenes: [
      'https://images.unsplash.com/photo-1639751787355-bbc3ed1fd639?w=1080&q=80',
      'https://images.unsplash.com/photo-1600047509807-ba8f99d2cdde?w=800&q=80',
      'https://images.unsplash.com/photo-1600566752355-35792bedcfea?w=800&q=80',
      'https://images.unsplash.com/photo-1598928506311-c55ded91a20c?w=800&q=80',
      'https://images.unsplash.com/photo-1622015663319-e97e697503ee?w=800&q=80',
    ],
    badge:    '',
    modo:     'compra',
    tipo:     'Casas',
    mascotas: true,
    descripcion: 'Espléndida casa familiar en el prestigioso sector de Santa Bárbara. Con cuatro amplias habitaciones, tres baños completos y doble garaje cubierto. El inmueble cuenta con un hermoso jardín trasero perfecto para niños y mascotas. Conjunto cerrado con piscina y club house.',
    caracteristicas: ['Jardín trasero', 'Piscina comunal', 'Club house', 'Doble garaje', 'Cuarto de servicio', 'Mascotas permitidas'],
    asesor: { nombre: 'Andrés Morales', telefono: '+57 315 234 5678', email: 'andres@reverinmobiliaria.com', inicial: 'A' },
  },
  {
    id: 4,
    titulo:        'Estudio Iluminado',
    precio_etiqueta: '$900.000/mes',
    precio:        900,
    ubicacion:     'Chapinero, Centro',
    direccion:     'Cl 63 #11-21, Chapinero, Bogotá',
    hab:  1, banos: 1, parqueaderos: 0, area: '45 m²',
    piso: 4, antiguedad: 2,
    imagenes: [
      'https://images.unsplash.com/photo-1763743305336-013be62c41d7?w=1080&q=80',
      'https://images.unsplash.com/photo-1505691938895-1758d7feb511?w=800&q=80',
      'https://images.unsplash.com/photo-1560448204-603b3fc33ddc?w=800&q=80',
      'https://images.unsplash.com/photo-1555041469-a586c61ea9bc?w=800&q=80',
    ],
    badge:    '',
    modo:     'arriendo',
    tipo:     'Estudios',
    mascotas: false,
    descripcion: 'Funcional estudio con diseño optimizado en Chapinero. Espacio abierto con zona de dormir, sala-comedor integrado y cocina americana. Excelente iluminación natural gracias a sus ventanales de piso a techo. A pasos del Parque de Lourdes.',
    caracteristicas: ['Ventanales de piso a techo', 'Cocina americana', 'Internet fibra óptica', 'Zona social edificio'],
    asesor: { nombre: 'Paula Ríos', telefono: '+57 318 765 4321', email: 'paula@reverinmobiliaria.com', inicial: 'P' },
  },
  {
    id: 5,
    titulo:        'Casa de Campo Serena',
    precio_etiqueta: '$195.000.000',
    precio:        195,
    ubicacion:     'Suba, Norte',
    direccion:     'Cra 92 #151-30, Suba, Bogotá',
    hab:  3, banos: 2, parqueaderos: 1, area: '150 m²',
    piso: 1, antiguedad: 8,
    imagenes: [
      'https://images.unsplash.com/photo-1758555226274-7b9f5c220b64?w=1080&q=80',
      'https://images.unsplash.com/photo-1568605114967-8130f3a36994?w=800&q=80',
      'https://images.unsplash.com/photo-1558618666-fcd25c85cd64?w=800&q=80',
      'https://images.unsplash.com/photo-1571939228382-b2f2b585ce15?w=800&q=80',
      'https://images.unsplash.com/photo-1567767292278-a4f21aa2d36e?w=800&q=80',
    ],
    badge:    'OPORTUNIDAD',
    modo:     'compra',
    tipo:     'Fincas',
    mascotas: true,
    descripcion: 'Acogedora casa de campo en Suba con amplio lote y jardines naturales. Tres habitaciones confortables, sala con chimenea y gran terraza con vista a la sabana. El barrio cuenta con colegios bilingües y centros comerciales cercanos.',
    caracteristicas: ['Chimenea', 'Jardín amplio', 'Lote grande', 'Terraza con vista', 'Mascotas permitidas', 'Portería 24h'],
    asesor: { nombre: 'Luis Herrera', telefono: '+57 301 111 2222', email: 'luis@reverinmobiliaria.com', inicial: 'L' },
  },
  {
    id: 6,
    titulo:        'Loft Diseño Cobre',
    precio_etiqueta: '$2.100.000/mes',
    precio:        2100,
    ubicacion:     'Usaquén, Norte',
    direccion:     'Cl 119 #6-50, Usaquén, Bogotá',
    hab:  1, banos: 1, parqueaderos: 1, area: '60 m²',
    piso: 3, antiguedad: 1,
    imagenes: [
      'https://images.unsplash.com/photo-1763419161907-1e00b2f883c5?w=1080&q=80',
      'https://images.unsplash.com/photo-1520608421967-8a36a8cbac7d?w=800&q=80',
      'https://images.unsplash.com/photo-1564078516393-cf04bd966897?w=800&q=80',
      'https://images.unsplash.com/photo-1616486338812-3dadae4b4ace?w=800&q=80',
    ],
    badge:    '',
    modo:     'arriendo',
    tipo:     'Apartamentos',
    mascotas: false,
    descripcion: 'Loft de diseño industrial con paleta de cobres y concreto expuesto en el corazón de Usaquén. Espacios diáfanos con doble altura y escalera en acero. Acabados de alta gama, iluminación arquitectónica y cocina gourmet.',
    caracteristicas: ['Doble altura', 'Cocina gourmet', 'Concreto expuesto', 'Parqueadero privado', 'Zona de coworking'],
    asesor: { nombre: 'Valentina Cruz', telefono: '+57 320 333 4444', email: 'valentina@reverinmobiliaria.com', inicial: 'V' },
  },
  {
    id: 7,
    titulo:        'Oficina Premium Centro',
    precio_etiqueta: '$3.500.000/mes',
    precio:        3500,
    ubicacion:     'Centro Empresarial, Norte',
    direccion:     'Av Cra 9 #115-06, Piso 8, Bogotá',
    hab:  0, banos: 2, parqueaderos: 3, area: '180 m²',
    piso: 8, antiguedad: 4,
    imagenes: [
      'https://images.unsplash.com/photo-1497366216548-37526070297c?w=1080&q=80',
      'https://images.unsplash.com/photo-1524758631624-e2822e304c36?w=800&q=80',
      'https://images.unsplash.com/photo-1497366811353-6870744d04b2?w=800&q=80',
      'https://images.unsplash.com/photo-1604328698692-f76ea9498e76?w=800&q=80',
    ],
    badge:    '',
    modo:     'arriendo',
    tipo:     'Oficinas',
    mascotas: false,
    descripcion: 'Moderna oficina corporativa de planta libre en el corazón del centro empresarial. 180 m² flexibles con sala de reuniones, recepción con vista panorámica y tres parqueaderos asignados. Edificio clase A con certificación LEED.',
    caracteristicas: ['Planta libre', 'Sala de reuniones', 'Vista panorámica', 'Certificación LEED', 'Generador eléctrico', 'Acceso inteligente'],
    asesor: { nombre: 'Diego Castillo', telefono: '+57 310 555 6666', email: 'diego@reverinmobiliaria.com', inicial: 'D' },
  },
  {
    id: 8,
    titulo:        'Finca Recreacional',
    precio_etiqueta: '$480.000.000',
    precio:        480,
    ubicacion:     'Vía La Calera',
    direccion:     'Vía La Calera km 4.5, Cundinamarca',
    hab:  4, banos: 4, parqueaderos: 4, area: '500 m²',
    piso: 1, antiguedad: 15,
    imagenes: [
      'https://images.unsplash.com/photo-1564013799919-ab600027ffc6?w=1080&q=80',
      'https://images.unsplash.com/photo-1588880331179-bc9b93a8cb5e?w=800&q=80',
      'https://images.unsplash.com/photo-1558618666-fcd25c85cd64?w=800&q=80',
      'https://images.unsplash.com/photo-1510627489930-0c1b0bfb6785?w=800&q=80',
      'https://images.unsplash.com/photo-1599809275671-b5942cabc7a2?w=800&q=80',
    ],
    badge:    'EXCLUSIVO',
    modo:     'compra',
    tipo:     'Fincas',
    mascotas: true,
    descripcion: 'Imponente finca recreacional sobre la vía La Calera con vistas espectaculares a los cerros orientales. Construcción en piedra y madera de ciprés, piscina climatizada, cancha de tenis y zona de BBQ. Lote de 5.000 m² con privacidad total.',
    caracteristicas: ['Piscina climatizada', 'Cancha de tenis', 'Zona BBQ', 'Chimenea', 'Lote 5.000 m²', 'Mascotas permitidas', 'Portería privada'],
    asesor: { nombre: 'Mariana López', telefono: '+57 316 777 8888', email: 'mariana@reverinmobiliaria.com', inicial: 'M' },
  },
  {
    id: 9,
    titulo:        'Apartamento con Vista',
    precio_etiqueta: '$285.000.000',
    precio:        285,
    ubicacion:     'Rosales, Norte',
    direccion:     'Cra 5 #83-29, El Rosales, Bogotá',
    hab:  3, banos: 2, parqueaderos: 2, area: '110 m²',
    piso: 12, antiguedad: 6,
    imagenes: [
      'https://images.unsplash.com/photo-1600585154340-be6161a56a0c?w=1080&q=80',
      'https://images.unsplash.com/photo-1560448204-603b3fc33ddc?w=800&q=80',
      'https://images.unsplash.com/photo-1616486338812-3dadae4b4ace?w=800&q=80',
      'https://images.unsplash.com/photo-1618221195710-dd6b41faaea6?w=800&q=80',
      'https://images.unsplash.com/photo-1598928506311-c55ded91a20c?w=800&q=80',
    ],
    badge:    '',
    modo:     'compra',
    tipo:     'Apartamentos',
    mascotas: false,
    descripcion: 'Elegante apartamento de tres habitaciones en El Rosales con vistas despejadas a los cerros orientales. Acabados premium con mármol importado, cocina de lujo y closets a medida. El edificio cuenta con conserjería 24 horas y zona de yoga en terraza.',
    caracteristicas: ['Vista a los cerros', 'Mármol importado', 'Conserjería 24h', 'Salón comunal', 'Zona de yoga', 'Doble parqueadero'],
    asesor: { nombre: 'Natalia Vargas', telefono: '+57 311 999 0000', email: 'natalia@reverinmobiliaria.com', inicial: 'N' },
  },
];


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
    valor: prop.area,
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
  /* Determinar qué propiedad mostrar */
  const id = obtener_id_desde_url();
  const propiedad = propiedades_db.find(p => p.id === id) ?? propiedades_db[0];

  /* Actualizar título del documento */
  document.title = `${propiedad.titulo} — Rever Inmobiliaria`;

  /* Montar contenido */
  renderizar_detalle(propiedad);

  /* Ocultar overlay y mostrar pantalla */
  setTimeout(() => {
    obtener_elemento_detalle('overlay_carga')?.classList.add('oculto');
    obtener_elemento_detalle('pantalla_detalle')?.classList.remove('oculto');
  }, 600);

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
}

/* Ejecutar al cargar el DOM */
document.addEventListener('DOMContentLoaded', inicializar_detalle);
