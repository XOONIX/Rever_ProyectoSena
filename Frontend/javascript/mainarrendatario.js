/* ═══════════════════════════════════════════════════════
   main.js — Rever Plataforma Inmobiliaria
   Convención: variables y funciones en español, snake_case
   ═══════════════════════════════════════════════════════ */

/* ──────────────────────────────────────────────────────
   1. ESTADO GLOBAL
   ────────────────────────────────────────────────────── */
const estado = {
  pantalla_actual: "arrendatario",   // "arrendatario" | "admin"
  vista_admin_actual: "tablero",     // "tablero" | "inmuebles" | "detalle" | "reportes" | "usuarios"
  filtro_inmuebles: "todos",
  filtro_usuarios: "todos",
  filtro_reportes: "todos",
  busqueda_inmuebles: "",
  busqueda_usuarios: "",
  inmueble_detalle_id: null,
  fotos_seleccionadas: [],
  amenidades_seleccionadas: new Set(["Piscina", "Gimnasio", "Garaje"]),
  contadores: { habitaciones: 3, banos: 2, parqueaderos: 1 },
};


/* ──────────────────────────────────────────────────────
   2. DATOS DE MUESTRA
   ────────────────────────────────────────────────────── */
const inmuebles = [
  {
    id: 1,
    nombre: "Penthouse Chapinero Alto",
    tipo: "Apartamento",
    ciudad: "Bogotá",
    direccion: "Cra 7 # 45-89",
    precio: "$8.500.000",
    estado: "verificada",
    propietario: "Carlos M.",
    correo_prop: "carlos@email.com",
    telefono_prop: "+57 300 123 4567",
    habitaciones: 4,
    banos: 3,
    parqueaderos: 2,
    area: 180,
    descripcion: "Espectacular penthouse con vista panorámica a los cerros orientales. Acabados de lujo en cada ambiente.",
    amenidades: ["Piscina", "Gimnasio", "Terraza", "Garaje", "Seguridad"],
    foto: "https://images.unsplash.com/photo-1512917774080-9991f1c4c750?w=400&h=300&fit=crop",
    fotos: [
      "https://images.unsplash.com/photo-1512917774080-9991f1c4c750?w=400&h=300&fit=crop",
      "https://images.unsplash.com/photo-1560448204-e02f11c3d0e2?w=400&h=300&fit=crop",
      "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?w=400&h=300&fit=crop",
    ],
    fecha: "15 Mar 2025",
  },
  {
    id: 2,
    nombre: "Casa Campestre El Retiro",
    tipo: "Casa",
    ciudad: "Medellín",
    direccion: "Km 3 Vía El Retiro",
    precio: "$12.000.000",
    estado: "pendiente",
    propietario: "Ana L.",
    correo_prop: "ana@email.com",
    telefono_prop: "+57 310 987 6543",
    habitaciones: 5,
    banos: 4,
    parqueaderos: 3,
    area: 320,
    descripcion: "Hermosa casa campestre con jardín amplio, rodeada de naturaleza.",
    amenidades: ["Jardín", "BBQ", "Garaje", "Seguridad"],
    foto: "https://images.unsplash.com/photo-1600596542815-ffad4c1539a9?w=400&h=300&fit=crop",
    fotos: [
      "https://images.unsplash.com/photo-1600596542815-ffad4c1539a9?w=400&h=300&fit=crop",
      "https://images.unsplash.com/photo-1600585154340-be6161a56a0c?w=400&h=300&fit=crop",
    ],
    fecha: "22 Mar 2025",
  },
  {
    id: 3,
    nombre: "Loft Zona Rosa",
    tipo: "Loft",
    ciudad: "Bogotá",
    direccion: "Cll 85 # 13-45",
    precio: "$5.200.000",
    estado: "rechazada",
    propietario: "Jorge P.",
    correo_prop: "jorge@email.com",
    telefono_prop: "+57 320 456 7890",
    habitaciones: 1,
    banos: 1,
    parqueaderos: 1,
    area: 65,
    descripcion: "Moderno loft en el corazón de la zona rosa. Ideal para profesionales.",
    amenidades: ["Gimnasio", "Concierge", "Garaje"],
    foto: "https://images.unsplash.com/photo-1502672023488-70e25813eb80?w=400&h=300&fit=crop",
    fotos: [
      "https://images.unsplash.com/photo-1502672023488-70e25813eb80?w=400&h=300&fit=crop",
    ],
    fecha: "10 Mar 2025",
  },
  {
    id: 4,
    nombre: "Apartamento Laureles",
    tipo: "Apartamento",
    ciudad: "Medellín",
    direccion: "Cra 80 # 35-60",
    precio: "$3.800.000",
    estado: "verificada",
    propietario: "María C.",
    correo_prop: "maria@email.com",
    telefono_prop: "+57 305 111 2233",
    habitaciones: 3,
    banos: 2,
    parqueaderos: 1,
    area: 95,
    descripcion: "Apartamento familiar en sector tranquilo de Laureles. Excelente iluminación.",
    amenidades: ["Piscina", "Garaje", "Portería"],
    foto: "https://images.unsplash.com/photo-1484154218962-a197022b5858?w=400&h=300&fit=crop",
    fotos: [
      "https://images.unsplash.com/photo-1484154218962-a197022b5858?w=400&h=300&fit=crop",
    ],
    fecha: "28 Feb 2025",
  },
  {
    id: 5,
    nombre: "Oficina Centro Empresarial",
    tipo: "Oficina",
    ciudad: "Cali",
    direccion: "Av. Roosevelt # 44-05",
    precio: "$4.500.000",
    estado: "pendiente",
    propietario: "Luis R.",
    correo_prop: "luis@email.com",
    telefono_prop: "+57 315 888 9900",
    habitaciones: 0,
    banos: 2,
    parqueaderos: 2,
    area: 120,
    descripcion: "Oficina moderna en piso 12 con vista a la ciudad. Incluye sala de juntas.",
    amenidades: ["Cafetería", "Garaje", "Seguridad", "AC"],
    foto: "https://images.unsplash.com/photo-1497366216548-37526070297c?w=400&h=300&fit=crop",
    fotos: [
      "https://images.unsplash.com/photo-1497366216548-37526070297c?w=400&h=300&fit=crop",
    ],
    fecha: "5 Abr 2025",
  },
];

const reportes = [
  {
    id: 1,
    tipo: "Fotos desactualizadas",
    inmueble: "Penthouse Chapinero Alto",
    inmueble_id: 1,
    reportante: "Pedro García",
    correo_rep: "pedro@email.com",
    descripcion: "Las fotos publicadas no corresponden al estado actual del inmueble. La pintura está deteriorada y hay humedad visible en la cocina.",
    evidencia: "https://images.unsplash.com/photo-1600585154340-be6161a56a0c?w=400&h=300&fit=crop",
    estado: "abierto",
    fecha: "2 Abr 2025",
    expandido: false,
  },
  {
    id: 2,
    tipo: "Propietario incorrecto",
    inmueble: "Casa Campestre El Retiro",
    inmueble_id: 2,
    reportante: "Laura Sánchez",
    correo_rep: "laura@email.com",
    descripcion: "El inmueble fue vendido hace 6 meses. El propietario actual es diferente al que aparece registrado.",
    evidencia: null,
    estado: "revision",
    fecha: "15 Mar 2025",
    expandido: false,
  },
  {
    id: 3,
    tipo: "Precio incorrecto",
    inmueble: "Loft Zona Rosa",
    inmueble_id: 3,
    reportante: "Andrés Torres",
    correo_rep: "andres@email.com",
    descripcion: "El precio publicado no coincide con el precio real negociado. Hay diferencia de $800.000.",
    evidencia: null,
    estado: "resuelto",
    fecha: "8 Mar 2025",
    expandido: false,
  },
  {
    id: 4,
    tipo: "Inmueble inexistente",
    inmueble: "Apartamento Laureles",
    inmueble_id: 4,
    reportante: "Sofía Martínez",
    correo_rep: "sofia@email.com",
    descripcion: "Visité el inmueble y la dirección no existe. Parece ser una publicación fraudulenta.",
    evidencia: "https://images.unsplash.com/photo-1497366216548-37526070297c?w=400&h=300&fit=crop",
    estado: "abierto",
    fecha: "1 Abr 2025",
    expandido: false,
  },
  {
    id: 5,
    tipo: "Fotos falsas",
    inmueble: "Oficina Centro Empresarial",
    inmueble_id: 5,
    reportante: "Camilo Ríos",
    correo_rep: "camilo@email.com",
    descripcion: "Las fotos corresponden a otro inmueble de otra ciudad. Se usaron imágenes de internet.",
    evidencia: null,
    estado: "descartado",
    fecha: "20 Feb 2025",
    expandido: false,
  },
];

const usuarios = [
  {
    id: 1,
    nombre: "Carlos Moreno",
    correo: "carlos@email.com",
    tipo: "propietario",
    inmuebles: 2,
    registro: "Ene 2025",
    req: { cedula: "completo", contrato: "completo", carta: "completo", foto_inmueble: "completo" },
    iniciales: "CM",
  },
  {
    id: 2,
    nombre: "Ana López",
    correo: "ana@email.com",
    tipo: "propietario",
    inmuebles: 1,
    registro: "Feb 2025",
    req: { cedula: "completo", contrato: "pendiente", carta: "incompleto", foto_inmueble: "completo" },
    iniciales: "AL",
  },
  {
    id: 3,
    nombre: "Jorge Pérez",
    correo: "jorge@email.com",
    tipo: "propietario",
    inmuebles: 1,
    registro: "Mar 2025",
    req: { cedula: "completo", contrato: "incompleto", carta: "incompleto", foto_inmueble: "pendiente" },
    iniciales: "JP",
  },
  {
    id: 4,
    nombre: "María Castro",
    correo: "maria@email.com",
    tipo: "comprador",
    inmuebles: 0,
    registro: "Ene 2025",
    req: { cedula: "completo", contrato: "completo", carta: "completo", foto_inmueble: "completo" },
    iniciales: "MC",
  },
  {
    id: 5,
    nombre: "Luis Ramírez",
    correo: "luis@email.com",
    tipo: "propietario",
    inmuebles: 1,
    registro: "Abr 2025",
    req: { cedula: "pendiente", contrato: "pendiente", carta: "pendiente", foto_inmueble: "pendiente" },
    iniciales: "LR",
  },
  {
    id: 6,
    nombre: "Valentina Díaz",
    correo: "valen@email.com",
    tipo: "comprador",
    inmuebles: 0,
    registro: "Mar 2025",
    req: { cedula: "completo", contrato: "completo", carta: "pendiente", foto_inmueble: "completo" },
    iniciales: "VD",
  },
  {
    id: 7,
    nombre: "Sebastián Gómez",
    correo: "sebas@email.com",
    tipo: "comprador",
    inmuebles: 0,
    registro: "Feb 2025",
    req: { cedula: "completo", contrato: "incompleto", carta: "completo", foto_inmueble: "completo" },
    iniciales: "SG",
  },
];


/* ──────────────────────────────────────────────────────
   3. TEXTOS / CONFIG
   ────────────────────────────────────────────────────── */
const config_estado_inmueble = {
  verificada: { texto: "Verificada", clase: "pildora_estado--verificada", color_punto: "#166534" },
  pendiente:  { texto: "Pendiente",  clase: "pildora_estado--pendiente",  color_punto: "#C9A84C" },
  rechazada:  { texto: "Rechazada",  clase: "pildora_estado--rechazada",  color_punto: "#991B1B" },
};

const config_estado_reporte = {
  abierto:    { texto: "Abierto",     clase: "pildora_estado--abierto",    color_punto: "#991B1B" },
  revision:   { texto: "En revisión", clase: "pildora_estado--revision",   color_punto: "#C9A84C" },
  resuelto:   { texto: "Resuelto",    clase: "pildora_estado--resuelto",   color_punto: "#166534" },
  descartado: { texto: "Descartado",  clase: "pildora_estado--descartado", color_punto: "#6B7280" },
};

const config_req = {
  cedula:       { etiqueta: "Cédula" },
  contrato:     { etiqueta: "Contrato" },
  carta:        { etiqueta: "Carta" },
  foto_inmueble:{ etiqueta: "Foto" },
};

const config_req_estado = {
  completo:   { clase: "card_usuario_movil__req--completo",   icono: "✓" },
  incompleto: { clase: "card_usuario_movil__req--incompleto", icono: "✕" },
  pendiente:  { clase: "card_usuario_movil__req--pendiente",  icono: "⏳" },
};

const lista_amenidades = [
  "Piscina", "Gimnasio", "Garaje", "Ascensor", "BBQ", "Terraza",
  "Jardín", "Seguridad", "Portería", "Concierge", "Lavandería",
  "Depósito", "Salón social", "Cancha", "Zona infantil",
];


/* ──────────────────────────────────────────────────────
   4. CAMBIO DE PANTALLA
   ────────────────────────────────────────────────────── */
function cambiar_pantalla(nombre_pantalla) {
  estado.pantalla_actual = nombre_pantalla;

  const pantalla_arr = document.getElementById("pantalla_arrendatario");
  const pantalla_adm = document.getElementById("pantalla_admin");

  if (nombre_pantalla === "arrendatario") {
    pantalla_arr.classList.add("pantalla--activa");
    pantalla_adm.classList.remove("pantalla--activa");
  } else {
    pantalla_arr.classList.remove("pantalla--activa");
    pantalla_adm.classList.add("pantalla--activa");
  }

  // Actualizar tabs del nav global
  document.querySelectorAll(".barra_navegacion__tab").forEach((tab) => {
    const es_activo = tab.dataset.pantalla === nombre_pantalla;
    tab.classList.toggle("barra_navegacion__tab--activo", es_activo);
  });

  if (nombre_pantalla === "admin") {
    cambiar_vista_admin(estado.vista_admin_actual);
  }
}


/* ──────────────────────────────────────────────────────
   5. CAMBIO DE VISTA ADMIN
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
    case "tablero":    renderizar_tablero(); break;
    case "inmuebles":  renderizar_inmuebles(); break;
    case "reportes":   renderizar_reportes(); break;
    case "usuarios":   renderizar_usuarios(); break;
  }
}

function actualizar_encabezado_admin(vista) {
  const titulos = {
    tablero:   "Panel de administración",
    inmuebles: "Gestión de inmuebles",
    detalle:   "Detalle del inmueble",
    reportes:  "Reportes",
    usuarios:  "Usuarios registrados",
  };

  const el_titulo = document.getElementById("encabezado_admin_titulo");
  const el_volver = document.getElementById("encabezado_admin_volver");

  if (el_titulo) el_titulo.textContent = titulos[vista] || "";
  if (el_volver) {
    el_volver.style.display = vista === "detalle" ? "flex" : "none";
  }
}


/* ──────────────────────────────────────────────────────
   6. RENDERIZAR TABLERO
   ────────────────────────────────────────────────────── */
function renderizar_tablero() {
  const total_verificadas = inmuebles.filter((i) => i.estado === "verificada").length;
  const total_pendientes  = inmuebles.filter((i) => i.estado === "pendiente").length;
  const total_rechazadas  = inmuebles.filter((i) => i.estado === "rechazada").length;
  const total_inmuebles   = inmuebles.length;
  const total_reportes    = reportes.filter((r) => r.estado === "abierto").length;

  const el = (id, val) => {
    const node = document.getElementById(id);
    if (node) node.textContent = val;
  };

  el("stat_verificadas", total_verificadas);
  el("stat_pendientes",  total_pendientes);
  el("stat_rechazadas",  total_rechazadas);
  el("stat_total",       total_inmuebles);
  el("stat_reportes",    total_reportes);

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


/* ──────────────────────────────────────────────────────
   7. RENDERIZAR INMUEBLES
   ────────────────────────────────────────────────────── */
function renderizar_inmuebles() {
  const filtrados = filtrar_inmuebles();

  renderizar_tabla_inmuebles(filtrados);
  renderizar_cards_inmuebles(filtrados);
}

function filtrar_inmuebles() {
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

function renderizar_tabla_inmuebles(lista) {
  const tbody = document.getElementById("tbody_inmuebles");
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
              <img src="assets/iconos/documento.svg" alt="" />
            </button>
            <button class="boton_accion boton_accion--editar" onclick="editar_inmueble(${i.id})" title="Editar" aria-label="Editar inmueble ${i.nombre}">
              <img src="assets/iconos/regla.svg" alt="" />
            </button>
            <button class="boton_accion boton_accion--eliminar" onclick="confirmar_eliminar_inmueble(${i.id})" title="Eliminar" aria-label="Eliminar inmueble ${i.nombre}">
              <img src="assets/iconos/cerrar.svg" alt="" />
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
            <img src="assets/iconos/documento.svg" alt="" /> Ver
          </button>
          <button class="card_movil__boton card_movil__boton--editar" onclick="editar_inmueble(${i.id})">
            <img src="assets/iconos/regla.svg" alt="" /> Editar
          </button>
          <button class="card_movil__boton card_movil__boton--eliminar" onclick="confirmar_eliminar_inmueble(${i.id})">
            <img src="assets/iconos/cerrar.svg" alt="" /> Eliminar
          </button>
        </div>
      </article>
    `;
  }).join("") || `<p style="text-align:center;padding:1.5rem;color:#9CA3AF;font-size:0.875rem;">Sin resultados</p>`;
}


/* ──────────────────────────────────────────────────────
   8. VER DETALLE DE INMUEBLE
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
            <img src="assets/iconos/verificado.svg" alt="" /> Verificar
          </button>
          <button class="boton_accion_detalle boton_accion_detalle--rechazar" onclick="cambiar_estado_inmueble(${inmueble_id},'rechazada')">
            <img src="assets/iconos/cerrar.svg" alt="" /> Rechazar
          </button>
          <button class="boton_accion_detalle boton_accion_detalle--eliminar" onclick="confirmar_eliminar_inmueble(${inmueble_id})">
            <img src="assets/iconos/cerrar.svg" alt="" /> Eliminar
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
                      <img src="assets/iconos/cerrar.svg" alt="Eliminar" />
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
                <img src="assets/iconos/mapa.svg" alt="" class="lista_detalles__icono" />
                <div>
                  <p class="lista_detalles__etiqueta">Ciudad</p>
                  <p class="lista_detalles__valor">${inmueble.ciudad}</p>
                </div>
              </div>
              <div class="lista_detalles__item">
                <img src="assets/iconos/mapa.svg" alt="" class="lista_detalles__icono" />
                <div>
                  <p class="lista_detalles__etiqueta">Dirección</p>
                  <p class="lista_detalles__valor">${inmueble.direccion}</p>
                </div>
              </div>
              <div class="lista_detalles__item">
                <img src="assets/iconos/edificio.svg" alt="" class="lista_detalles__icono" />
                <div>
                  <p class="lista_detalles__etiqueta">Tipo</p>
                  <p class="lista_detalles__valor">${inmueble.tipo}</p>
                </div>
              </div>
              <div class="lista_detalles__item">
                <img src="assets/iconos/regla.svg" alt="" class="lista_detalles__icono" />
                <div>
                  <p class="lista_detalles__etiqueta">Área</p>
                  <p class="lista_detalles__valor">${inmueble.area} m²</p>
                </div>
              </div>
              <div class="lista_detalles__item">
                <img src="assets/iconos/dinero.svg" alt="" class="lista_detalles__icono" />
                <div>
                  <p class="lista_detalles__etiqueta">Precio/mes</p>
                  <p class="lista_detalles__valor">${inmueble.precio}</p>
                </div>
              </div>
              ${inmueble.habitaciones > 0 ? `
              <div class="lista_detalles__item">
                <img src="assets/iconos/casa.svg" alt="" class="lista_detalles__icono" />
                <div>
                  <p class="lista_detalles__etiqueta">Habitaciones</p>
                  <p class="lista_detalles__valor">${inmueble.habitaciones}</p>
                </div>
              </div>` : ""}
              <div class="lista_detalles__item">
                <img src="assets/iconos/info.svg" alt="" class="lista_detalles__icono" />
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
              <div class="info_propietario__avatar">${inmueble.propietario.split(" ").map((p) => p[0]).join("").slice(0,2)}</div>
              <div>
                <p class="info_propietario__nombre">${inmueble.propietario}</p>
                <p class="info_propietario__cargo">Propietario</p>
              </div>
            </div>
            <div class="info_propietario__contacto">
              <div class="info_propietario__linea">
                <img src="assets/iconos/correo.svg" alt="" />
                <span>${inmueble.correo_prop}</span>
              </div>
              <div class="info_propietario__linea">
                <img src="assets/iconos/telefono.svg" alt="" />
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
   9. RENDERIZAR REPORTES
   ────────────────────────────────────────────────────── */
function renderizar_reportes() {
  actualizar_resumen_reportes();
  renderizar_lista_reportes();
}

function actualizar_resumen_reportes() {
  const conteos = { abierto: 0, revision: 0, resuelto: 0, descartado: 0 };
  reportes.forEach((r) => { if (conteos[r.estado] !== undefined) conteos[r.estado]++; });

  const el = (id, val) => { const n = document.getElementById(id); if (n) n.textContent = val; };
  el("resumen_abiertos",    conteos.abierto);
  el("resumen_revision",    conteos.revision);
  el("resumen_resueltos",   conteos.resuelto);
  el("resumen_descartados", conteos.descartado);
}

function renderizar_lista_reportes() {
  const contenedor = document.getElementById("lista_reportes_contenedor");
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
            <img src="assets/iconos/alerta.svg" alt="" />
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
                <img src="assets/iconos/edificio.svg" alt="" /> Ver inmueble
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
   10. RENDERIZAR USUARIOS
   ────────────────────────────────────────────────────── */
function renderizar_usuarios() {
  const filtrados = filtrar_usuarios();
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
  const tbody = document.getElementById("tbody_usuarios");
  if (!tbody) return;

  const req_keys = Object.keys(config_req);

  tbody.innerHTML = lista.map((u) => {
    const tipo_cfg = u.tipo === "propietario"
      ? { clase: "pildora_estado--propietario", texto: "Propietario" }
      : { clase: "pildora_estado--comprador", texto: "Comprador" };

    const req_html = req_keys.map((key) => {
      const est = u.req[key];
      const colores = { completo: "#166534", incompleto: "#991B1B", pendiente: "#C9A84C" };
      const color = colores[est] || "#6B7280";
      return `<span title="${config_req[key].etiqueta}: ${est}" style="color:${color};font-size:0.7rem;font-weight:600;margin-right:0.375rem;">${config_req[key].etiqueta.substring(0,3)}</span>`;
    }).join("");

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
        <td style="font-size:0.75rem;">${req_html}</td>
        <td class="celda_meta">${u.inmuebles} inmueble${u.inmuebles !== 1 ? "s" : ""}</td>
        <td class="celda_meta">${u.registro}</td>
        <td>
          <div class="celda_acciones">
            <button class="boton_accion boton_accion--ver" title="Ver usuario" aria-label="Ver usuario ${u.nombre}">
              <img src="assets/iconos/usuario.svg" alt="" />
            </button>
            <button class="boton_accion boton_accion--eliminar" title="Eliminar usuario" aria-label="Eliminar usuario ${u.nombre}">
              <img src="assets/iconos/cerrar.svg" alt="" />
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
    const req_keys = Object.keys(config_req);
    const req_html = req_keys.map((key) => {
      const est = u.req[key];
      const cfg_est = config_req_estado[est] || {};
      return `<span class="card_usuario_movil__req ${cfg_est.clase}">${cfg_est.icono || ""} ${config_req[key].etiqueta}</span>`;
    }).join("");

    return `
      <article class="card_usuario_movil">
        <div class="card_usuario_movil__cabecera">
          <div class="card_usuario_movil__avatar">${u.iniciales}</div>
          <div>
            <p class="card_usuario_movil__nombre">${u.nombre}</p>
            <p class="card_usuario_movil__correo">${u.correo} · ${tipo_texto}</p>
          </div>
        </div>
        <div class="card_usuario_movil__requisitos">${req_html}</div>
      </article>
    `;
  }).join("") || `<p style="text-align:center;padding:1.5rem;color:#9CA3AF;font-size:0.875rem;">Sin usuarios</p>`;
}


/* ──────────────────────────────────────────────────────
   11. FORMULARIO ARRENDATARIO
   ────────────────────────────────────────────────────── */
function incrementar_contador(campo) {
  estado.contadores[campo]++;
  actualizar_visualizacion_contador(campo);
}

function decrementar_contador(campo) {
  const minimo = campo === "parqueaderos" ? 0 : 1;
  if (estado.contadores[campo] > minimo) {
    estado.contadores[campo]--;
    actualizar_visualizacion_contador(campo);
  }
}

function actualizar_visualizacion_contador(campo) {
  const el = document.getElementById(`contador_${campo}`);
  if (el) el.textContent = estado.contadores[campo];
}

function actualizar_contador_descripcion() {
  const textarea = document.getElementById("descripcion_inmueble");
  const contador = document.getElementById("contador_descripcion");
  if (textarea && contador) {
    contador.textContent = `${textarea.value.length}/500`;
  }
}

function alternar_amenidad(nombre_amenidad) {
  if (estado.amenidades_seleccionadas.has(nombre_amenidad)) {
    estado.amenidades_seleccionadas.delete(nombre_amenidad);
  } else {
    estado.amenidades_seleccionadas.add(nombre_amenidad);
  }
  renderizar_amenidades();
}

function renderizar_amenidades() {
  const contenedor = document.getElementById("grupo_amenidades");
  if (!contenedor) return;

  contenedor.innerHTML = lista_amenidades.map((amenidad) => {
    const seleccionada = estado.amenidades_seleccionadas.has(amenidad);
    return `
      <button
        class="chip_amenidad ${seleccionada ? 'chip_amenidad--seleccionado' : ''}"
        onclick="alternar_amenidad('${amenidad}')"
        aria-pressed="${seleccionada}"
        type="button"
      >
        ${seleccionada ? '<span class="chip_amenidad__icono_check">✓</span>' : ""}
        ${amenidad}
      </button>
    `;
  }).join("");
}

function abrir_mapa() {
  const ciudad = document.getElementById("ciudad_inmueble")?.value || "";
  const direccion = document.getElementById("direccion_inmueble")?.value || "";
  const consulta = encodeURIComponent(`${ciudad} ${direccion}`.trim() || "Colombia");
  window.open(`https://www.google.com/maps/search/${consulta}`, "_blank", "noopener");
}

function agregar_fotos_seleccionadas(evento) {
  const archivos = Array.from(evento.target.files || []);
  archivos.forEach((archivo) => {
    const url = URL.createObjectURL(archivo);
    estado.fotos_seleccionadas.push({ nombre: archivo.name, url });
  });
  renderizar_galeria_fotos();
}

function manejar_clic_zona_carga() {
  const input = document.getElementById("input_fotos");
  if (input) input.click();
}

function manejar_arrastre_sobre(evento) {
  evento.preventDefault();
  const zona = document.getElementById("zona_carga_fotos");
  if (zona) zona.classList.add("zona_carga--arrastrando");
}

function manejar_salida_arrastre() {
  const zona = document.getElementById("zona_carga_fotos");
  if (zona) zona.classList.remove("zona_carga--arrastrando");
}

function manejar_soltar_fotos(evento) {
  evento.preventDefault();
  manejar_salida_arrastre();
  const archivos = Array.from(evento.dataTransfer?.files || []).filter((f) => f.type.startsWith("image/"));
  archivos.forEach((archivo) => {
    const url = URL.createObjectURL(archivo);
    estado.fotos_seleccionadas.push({ nombre: archivo.name, url });
  });
  renderizar_galeria_fotos();
}

function eliminar_foto(indice) {
  URL.revokeObjectURL(estado.fotos_seleccionadas[indice]?.url);
  estado.fotos_seleccionadas.splice(indice, 1);
  renderizar_galeria_fotos();
}

function renderizar_galeria_fotos() {
  const galeria = document.getElementById("galeria_fotos_previas");
  if (!galeria) return;

  if (estado.fotos_seleccionadas.length === 0) {
    galeria.innerHTML = "";
    galeria.style.display = "none";
    return;
  }

  galeria.style.display = "grid";
  galeria.innerHTML = estado.fotos_seleccionadas.map((foto, idx) => `
    <li class="galeria_fotos__miniatura">
      <img src="${foto.url}" alt="${foto.nombre}" class="galeria_fotos__imagen" />
      ${idx === 0 ? '<span class="galeria_fotos__etiqueta_principal">Principal</span>' : ""}
      <div class="galeria_fotos__superposicion">
        <button class="galeria_fotos__boton_eliminar" onclick="eliminar_foto(${idx})" aria-label="Eliminar foto">✕</button>
      </div>
    </li>
  `).join("") + `
    <button class="galeria_fotos__agregar" onclick="manejar_clic_zona_carga()" type="button">
      <span class="galeria_fotos__agregar_icono">+</span>
      <span class="galeria_fotos__agregar_texto">Agregar</span>
    </button>
  `;
}

function manejar_envio_formulario(evento) {
  evento.preventDefault();
  const numero_radicado = `RV-${Date.now().toString().slice(-6)}`;
  const modal = document.getElementById("modal_confirmacion");
  const el_radicado = document.getElementById("numero_radicado");
  if (modal) modal.classList.remove("modal_confirmacion--oculto");
  if (el_radicado) el_radicado.textContent = numero_radicado;
}

function cerrar_modal_confirmacion() {
  const modal = document.getElementById("modal_confirmacion");
  if (modal) modal.classList.add("modal_confirmacion--oculto");
}

function nueva_publicacion() {
  cerrar_modal_confirmacion();
  const formulario = document.getElementById("formulario_arrendatario");
  if (formulario) formulario.reset();
  // Limpiar estado del formulario
  estado.fotos_seleccionadas = [];
  estado.amenidades_seleccionadas = new Set(["Piscina", "Gimnasio", "Garaje"]);
  estado.contadores = { habitaciones: 3, banos: 2, parqueaderos: 1 };
  renderizar_galeria_fotos();
  renderizar_amenidades();
  ["habitaciones", "banos", "parqueaderos"].forEach(actualizar_visualizacion_contador);
}


/* ──────────────────────────────────────────────────────
   12. FILTROS Y BÚSQUEDA
   ────────────────────────────────────────────────────── */
function cambiar_filtro_inmuebles(nuevo_filtro) {
  estado.filtro_inmuebles = nuevo_filtro;
  document.querySelectorAll("[data-filtro-inmueble]").forEach((btn) => {
    btn.classList.toggle("boton_filtro--activo", btn.dataset.filtroInmueble === nuevo_filtro);
  });
  renderizar_inmuebles();
}

function cambiar_filtro_reportes(nuevo_filtro) {
  estado.filtro_reportes = nuevo_filtro;
  document.querySelectorAll("[data-filtro-reporte]").forEach((btn) => {
    btn.classList.toggle("boton_filtro--activo", btn.dataset.filtroReporte === nuevo_filtro);
  });
  renderizar_lista_reportes();
}

function cambiar_filtro_usuarios(nuevo_filtro) {
  estado.filtro_usuarios = nuevo_filtro;
  document.querySelectorAll("[data-filtro-usuario]").forEach((btn) => {
    btn.classList.toggle("boton_filtro--activo", btn.dataset.filtroUsuario === nuevo_filtro);
  });
  renderizar_usuarios();
}

function buscar_inmuebles(termino) {
  estado.busqueda_inmuebles = termino;
  renderizar_inmuebles();
}

function buscar_usuarios(termino) {
  estado.busqueda_usuarios = termino;
  renderizar_usuarios();
}


/* ──────────────────────────────────────────────────────
   13. INICIALIZACIÓN
   ────────────────────────────────────────────────────── */
function inicializar() {
  // Tabs del nav global
  document.querySelectorAll(".barra_navegacion__tab").forEach((tab) => {
    tab.addEventListener("click", () => cambiar_pantalla(tab.dataset.pantalla));
    tab.setAttribute("role", "button");
  });

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

  // Tarjeta de reportes en tablero
  const tarjeta_reportes = document.getElementById("tarjeta_stat_reportes");
  if (tarjeta_reportes) {
    tarjeta_reportes.addEventListener("click", () => cambiar_vista_admin("reportes"));
  }

  // Botón volver en detalle
  const btn_volver = document.getElementById("encabezado_admin_volver");
  if (btn_volver) {
    btn_volver.addEventListener("click", volver_a_listado);
  }

  // Formulario arrendatario
  const formulario = document.getElementById("formulario_arrendatario");
  if (formulario) {
    formulario.addEventListener("submit", manejar_envio_formulario);
  }

  // Textarea descripción
  const textarea_desc = document.getElementById("descripcion_inmueble");
  if (textarea_desc) {
    textarea_desc.addEventListener("input", actualizar_contador_descripcion);
  }

  // Zona de carga de fotos
  const zona_carga = document.getElementById("zona_carga_fotos");
  if (zona_carga) {
    zona_carga.addEventListener("click", manejar_clic_zona_carga);
    zona_carga.addEventListener("dragover", manejar_arrastre_sobre);
    zona_carga.addEventListener("dragleave", manejar_salida_arrastre);
    zona_carga.addEventListener("drop", manejar_soltar_fotos);
  }

  // Input de fotos oculto
  const input_fotos = document.getElementById("input_fotos");
  if (input_fotos) {
    input_fotos.addEventListener("change", agregar_fotos_seleccionadas);
  }

  // Botón mapa
  const btn_mapa = document.getElementById("btn_abrir_mapa");
  if (btn_mapa) {
    btn_mapa.addEventListener("click", abrir_mapa);
  }

  // Modal confirmación
  const btn_cerrar_modal = document.getElementById("btn_cerrar_modal");
  if (btn_cerrar_modal) btn_cerrar_modal.addEventListener("click", cerrar_modal_confirmacion);

  const btn_nueva = document.getElementById("btn_nueva_publicacion");
  if (btn_nueva) btn_nueva.addEventListener("click", nueva_publicacion);

  // Filtros de inmuebles
  document.querySelectorAll("[data-filtro-inmueble]").forEach((btn) => {
    btn.addEventListener("click", () => cambiar_filtro_inmuebles(btn.dataset.filtroInmueble));
  });

  // Filtros de reportes
  document.querySelectorAll("[data-filtro-reporte]").forEach((btn) => {
    btn.addEventListener("click", () => cambiar_filtro_reportes(btn.dataset.filtroReporte));
  });

  // Filtros de usuarios
  document.querySelectorAll("[data-filtro-usuario]").forEach((btn) => {
    btn.addEventListener("click", () => cambiar_filtro_usuarios(btn.dataset.filtroUsuario));
  });

  // Búsquedas
  const busq_inmuebles = document.getElementById("busqueda_inmuebles");
  if (busq_inmuebles) busq_inmuebles.addEventListener("input", (e) => buscar_inmuebles(e.target.value));

  const busq_usuarios = document.getElementById("busqueda_usuarios");
  if (busq_usuarios) busq_usuarios.addEventListener("input", (e) => buscar_usuarios(e.target.value));

  // Contadores numéricos
  document.querySelectorAll("[data-contador]").forEach((btn) => {
    const campo = btn.dataset.contador;
    const accion = btn.dataset.accion;
    btn.addEventListener("click", () => {
      if (accion === "incrementar") incrementar_contador(campo);
      else decrementar_contador(campo);
    });
  });

  // Inicializar renders de UI dinámica
  renderizar_amenidades();
  ["habitaciones", "banos", "parqueaderos"].forEach(actualizar_visualizacion_contador);

  // Activar pantalla inicial
  cambiar_pantalla("arrendatario");
}

// Arrancar cuando el DOM esté listo
if (document.readyState === "loading") {
  document.addEventListener("DOMContentLoaded", inicializar);
} else {
  inicializar();
}
