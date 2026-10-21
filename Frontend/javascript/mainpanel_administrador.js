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


/* ──────────────────────────────────────────────────────
   6. RENDERIZAR INMUEBLES
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

function filtrar_inmuebles() {
  const input = document.getElementById("buscador_inmuebles");
  if (input) {
    estado.busqueda_inmuebles = input.value;
    renderizar_inmuebles();
  }
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

function cerrar_menu_usuario_admin() {
  if (estado.menu_usuario_admin_abierto) {
    estado.menu_usuario_admin_abierto = false;

    const menu = document.getElementById("menu_usuario_admin");
    const boton = document.querySelector(".barra_lateral__avatar--clickeable");

    if (menu) {
      menu.classList.add("menu_usuario_admin--oculto");
    }

    if (boton) {
      boton.setAttribute("aria-expanded", "false");
    }
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
function inicializar() {
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