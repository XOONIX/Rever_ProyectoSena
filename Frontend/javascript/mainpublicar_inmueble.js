/* ═══════════════════════════════════════════════════════
   mainpublicar_inmueble.js — Rever Plataforma Inmobiliaria
   Conectado a la base de datos real vía fetch
   ═══════════════════════════════════════════════════════ */

const estado_publicar = {
  fotos_seleccionadas: [],
  caracteristicas_disponibles: [],
  caracteristicas_seleccionadas: new Set(),
  contadores: { habitaciones: 1, banios: 1, parqueaderos: 0, piso: 1 },
  catalogo_barrios: [],
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
   3. CASCADA CIUDAD → LOCALIDAD (si aplica) → BARRIO
   ────────────────────────────────────────────────────── */
function manejar_cambio_ciudad() {
  const select_ciudad = document.getElementById('ciudad');
  const contenedor_localidad = document.getElementById('contenedor_localidad');
  const select_localidad = document.getElementById('localidad');
  const select_barrio = document.getElementById('barrio');

  const idCiudad = parseInt(select_ciudad.value, 10);
  const nombre_ciudad = select_ciudad.value ? select_ciudad.options[select_ciudad.selectedIndex].text : null;

  select_localidad.innerHTML = '<option value="" disabled selected>Selecciona localidad</option>';
  select_barrio.innerHTML = '<option value="" disabled selected>Selecciona barrio</option>';
  select_barrio.disabled = true;

  if (!idCiudad) {
    contenedor_localidad.classList.add('oculto');
    return;
  }

  const barrios_de_la_ciudad = estado_publicar.catalogo_barrios.filter(b => b.idCiudad === idCiudad);
  const ids_localidad_unicos = [...new Set(barrios_de_la_ciudad.map(b => b.idLocalidad))];

  // Solo mostramos el paso de Localidad cuando la ciudad tiene más de una localidad real (caso Bogotá)
  if (nombre_ciudad === 'Bogotá' && ids_localidad_unicos.length > 1) {
    contenedor_localidad.classList.remove('oculto');
    ids_localidad_unicos.forEach(idLoc => {
      const localidad = estado_publicar.catalogo_localidades.find(l => l.idLocalidad === idLoc);
      if (localidad) select_localidad.add(new Option(localidad.nombre, localidad.idLocalidad));
    });
  } else {
    contenedor_localidad.classList.add('oculto');
    barrios_de_la_ciudad.forEach(b => select_barrio.add(new Option(b.nombre, b.idBarrio)));
    select_barrio.disabled = false;
  }
}

function manejar_cambio_localidad() {
  const select_ciudad = document.getElementById('ciudad');
  const select_localidad = document.getElementById('localidad');
  const select_barrio = document.getElementById('barrio');

  const idCiudad = parseInt(select_ciudad.value, 10);
  const idLocalidad = parseInt(select_localidad.value, 10);

  select_barrio.innerHTML = '<option value="" disabled selected>Selecciona barrio</option>';

  if (!idLocalidad) {
    select_barrio.disabled = true;
    return;
  }

  const barrios_filtrados = estado_publicar.catalogo_barrios.filter(
    b => b.idCiudad === idCiudad && b.idLocalidad === idLocalidad
  );
  barrios_filtrados.forEach(b => select_barrio.add(new Option(b.nombre, b.idBarrio)));
  select_barrio.disabled = false;
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
   7. CARGA DE CATÁLOGOS REALES (al abrir la página)
   ────────────────────────────────────────────────────── */
async function cargar_catalogos() {
  try {
    const [tipos, modos, ciudades, caracteristicas, barrios, localidades] = await Promise.all([
      fetch(`${API_URL}/TipoInmueble`).then(r => r.json()),
      fetch(`${API_URL}/ModoTransaccion`).then(r => r.json()),
      fetch(`${API_URL}/Ciudad`).then(r => r.json()),
      fetch(`${API_URL}/Caracteristica`).then(r => r.json()),
      fetch(`${API_URL}/Barrio`).then(r => r.json()),
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
    estado_publicar.catalogo_barrios = barrios;
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
    idBarrio: obtener_entero_select('barrio'),

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
function inicializar() {
  const formulario = document.getElementById('formulario_publicacion');
  if (formulario) {
    // Único punto de envío (el HTML ya no tiene onsubmit)
    formulario.addEventListener('submit', manejar_envio_formulario);
  }

  const select_ciudad = document.getElementById('ciudad');
  if (select_ciudad) {
    select_ciudad.addEventListener('change', manejar_cambio_ciudad);
  }

  const select_localidad = document.getElementById('localidad');
  if (select_localidad) {
    select_localidad.addEventListener('change', manejar_cambio_localidad);
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