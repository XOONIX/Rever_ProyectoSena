/* ═══════════════════════════════════════════════════════
   mainpublicar_inmueble.js — Rever Plataforma Inmobiliaria
   Funcionalidad específica para la sección de Publicar Inmueble
   Convención: variables y funciones en español, snake_case
   ═══════════════════════════════════════════════════════ */

/* ──────────────────────────────────────────────────────
   1. ESTADO GLOBAL
   ────────────────────────────────────────────────────── */
const estado = {
  fotos_seleccionadas: [],
  amenidades_seleccionadas: new Set(["Piscina", "Gimnasio", "Garaje"]),
  contadores: { habitaciones: 3, banos: 2, parqueaderos: 1, piso: 1 },
};


/* ──────────────────────────────────────────────────────
   2. CONFIGURACIÓN
   ────────────────────────────────────────────────────── */
const lista_amenidades = [
  "Piscina", "Gimnasio", "Garaje", "Ascensor", "BBQ", "Terraza",
  "Jardín", "Seguridad", "Portería", "Concierge", "Lavandería",
  "Depósito", "Salón social", "Cancha", "Zona infantil",
];

const localidades_bogota = [
  "Usaquén",
  "Chapinero",
  "Santa Fe",
  "San Cristóbal",
  "Usme",
  "Tunjuelito",
  "Bosa",
  "Kennedy",
  "Fontibón",
  "Engativá",
  "Suba",
  "Barrios Unidos",
  "Teusaquillo",
  "Los Mártires",
  "Antonio Nariño",
  "Puente Aranda",
  "La Candelaria",
  "Rafael Uribe Uribe",
  "Ciudad Bolívar",
  "Sumapaz (localidad rural)"
];

const comunas_medellin = [
  "Comuna 1 – Popular",
  "Comuna 2 – Santa Cruz",
  "Comuna 3 – Manrique",
  "Comuna 4 – Aranjuez",
  "Comuna 5 – Castilla",
  "Comuna 6 – Doce de Octubre",
  "Comuna 7 – Robledo",
  "Comuna 8 – Villa Hermosa",
  "Comuna 9 – Buenos Aires",
  "Comuna 10 – La Candelaria (Centro de la ciudad)",
  "Comuna 11 – Laureles - Estadio",
  "Comuna 12 – La América",
  "Comuna 13 – San Javier",
  "Comuna 14 – El Poblado",
  "Comuna 15 – Guayabal",
  "Comuna 16 – Belén"
];

const corregimientos_medellin = [
  "San Cristóbal",
  "San Sebastián de Palmitas",
  "San Antonio de Prado",
  "Santa Elena",
  "Altavista"
];


/* ──────────────────────────────────────────────────────
   3. CONTADORES NUMÉRICOS
   ────────────────────────────────────────────────────── */
function incrementar_contador(campo) {
  estado.contadores[campo]++;
  actualizar_visualizacion_contador(campo);
}

function decrementar_contador(campo, minimo) {
  const valor_minimo = minimo !== undefined ? minimo : (campo === "parqueaderos" ? 0 : 1);
  if (estado.contadores[campo] > valor_minimo) {
    estado.contadores[campo]--;
    actualizar_visualizacion_contador(campo);
  }
}

function actualizar_visualizacion_contador(campo) {
  const el = document.getElementById(campo);
  if (el) el.textContent = estado.contadores[campo];
}


/* ──────────────────────────────────────────────────────
   4. AMENIDADES
   ────────────────────────────────────────────────────── */
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


/* ──────────────────────────────────────────────────────
   5. LOCALIDAD
   ────────────────────────────────────────────────────── */
function manejar_cambio_ciudad() {
  const ciudad = document.getElementById("ciudad")?.value;
  const contenedor_localidad = document.getElementById("contenedor_localidad");
  const select_localidad = document.getElementById("localidad");
  const contenedor_tipo_zona_medellin = document.getElementById("contenedor_tipo_zona_medellin");
  const contenedor_zona_medellin = document.getElementById("contenedor_zona_medellin");
  const select_tipo_zona_medellin = document.getElementById("tipo_zona_medellin");
  const select_zona_medellin = document.getElementById("zona_medellin");

  // Resetear todos los campos condicionales
  if (contenedor_localidad) contenedor_localidad.style.display = "none";
  if (contenedor_tipo_zona_medellin) contenedor_tipo_zona_medellin.style.display = "none";
  if (contenedor_zona_medellin) contenedor_zona_medellin.style.display = "none";

  if (select_localidad) {
    select_localidad.innerHTML = '<option value="" disabled selected>Selecciona localidad</option>';
    select_localidad.required = false;
    select_localidad.removeAttribute("aria-required");
  }

  if (select_tipo_zona_medellin) {
    select_tipo_zona_medellin.value = "";
    select_tipo_zona_medellin.required = false;
    select_tipo_zona_medellin.removeAttribute("aria-required");
  }

  if (select_zona_medellin) {
    select_zona_medellin.innerHTML = '<option value="" disabled selected>Selecciona zona</option>';
    select_zona_medellin.required = false;
    select_zona_medellin.removeAttribute("aria-required");
  }

  if (ciudad === "bogota") {
    // Mostrar campo de localidad y poblar con localidades de Bogotá
    if (contenedor_localidad) contenedor_localidad.style.display = "block";
    if (select_localidad) {
      select_localidad.innerHTML = '<option value="" disabled selected>Selecciona localidad</option>';
      select_localidad.required = true;
      select_localidad.setAttribute("aria-required", "true");
      localidades_bogota.forEach(localidad => {
        const option = document.createElement("option");
        option.value = localidad;
        option.textContent = localidad;
        select_localidad.appendChild(option);
      });
    }
  } else if (ciudad === "medellin") {
    // Mostrar campo de tipo de zona para Medellín
    if (contenedor_tipo_zona_medellin) contenedor_tipo_zona_medellin.style.display = "block";
    if (select_tipo_zona_medellin) {
      select_tipo_zona_medellin.required = true;
      select_tipo_zona_medellin.setAttribute("aria-required", "true");
    }
  }
}

function manejar_cambio_tipo_zona_medellin() {
  const tipo_zona = document.getElementById("tipo_zona_medellin")?.value;
  const contenedor_zona_medellin = document.getElementById("contenedor_zona_medellin");
  const select_zona_medellin = document.getElementById("zona_medellin");
  const etiqueta_zona_medellin = document.getElementById("etiqueta_zona_medellin");

  if (tipo_zona === "comuna") {
    // Mostrar campo de comuna y poblar con comunas de Medellín
    if (contenedor_zona_medellin) contenedor_zona_medellin.style.display = "block";
    if (etiqueta_zona_medellin) etiqueta_zona_medellin.textContent = "Comuna";
    if (select_zona_medellin) {
      select_zona_medellin.innerHTML = '<option value="" disabled selected>Selecciona comuna</option>';
      select_zona_medellin.required = true;
      select_zona_medellin.setAttribute("aria-required", "true");
      comunas_medellin.forEach(comuna => {
        const option = document.createElement("option");
        option.value = comuna;
        option.textContent = comuna;
        select_zona_medellin.appendChild(option);
      });
    }
  } else if (tipo_zona === "corregimiento") {
    // Mostrar campo de corregimiento y poblar con corregimientos de Medellín
    if (contenedor_zona_medellin) contenedor_zona_medellin.style.display = "block";
    if (etiqueta_zona_medellin) etiqueta_zona_medellin.textContent = "Corregimiento";
    if (select_zona_medellin) {
      select_zona_medellin.innerHTML = '<option value="" disabled selected>Selecciona corregimiento</option>';
      select_zona_medellin.required = true;
      select_zona_medellin.setAttribute("aria-required", "true");
      corregimientos_medellin.forEach(corregimiento => {
        const option = document.createElement("option");
        option.value = corregimiento;
        option.textContent = corregimiento;
        select_zona_medellin.appendChild(option);
      });
    }
  } else {
    // Ocultar campo de zona
    if (contenedor_zona_medellin) contenedor_zona_medellin.style.display = "none";
    if (select_zona_medellin) {
      select_zona_medellin.innerHTML = '<option value="" disabled selected>Selecciona zona</option>';
      select_zona_medellin.required = false;
      select_zona_medellin.removeAttribute("aria-required");
    }
  }
}

/* ──────────────────────────────────────────────────────
   7. MAPA
   ────────────────────────────────────────────────────── */
function abrir_mapa() {
  const ciudad = document.getElementById("ciudad")?.value || "";
  const direccion = document.getElementById("direccion")?.value || "";
  const consulta = encodeURIComponent(`${ciudad} ${direccion}`.trim() || "Colombia");
  window.open(`https://www.google.com/maps/search/${consulta}`, "_blank", "noopener");
}


/* ──────────────────────────────────────────────────────
   8. FOTOGRAFÍAS
   ────────────────────────────────────────────────────── */
function agregar_fotos_seleccionadas(evento) {
  const archivos = Array.from(evento.target.files || []);
  archivos.forEach((archivo) => {
    const url = URL.createObjectURL(archivo);
    estado.fotos_seleccionadas.push({ nombre: archivo.name, url });
  });
  renderizar_galeria_fotos();
}

function manejar_arrastre(evento, arrastrando) {
  evento.preventDefault();
  const zona = document.getElementById("zona_arrastre");
  if (zona) {
    zona.classList.toggle("zona_carga--arrastrando", arrastrando);
  }
}

function manejar_soltar_archivos(evento) {
  evento.preventDefault();
  manejar_arrastre(evento, false);
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
  const galeria = document.getElementById("galeria_fotos");
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
  `).join("");
  
  // Actualizar contador
  const contador = document.getElementById("contador_fotos");
  if (contador) {
    contador.textContent = `${estado.fotos_seleccionadas.length} / 10 fotos cargadas. La primera foto será la imagen principal.`;
  }
}


/* ──────────────────────────────────────────────────────
   9. DESCRIPCIÓN
   ────────────────────────────────────────────────────── */
function actualizar_contador_descripcion() {
  const textarea = document.getElementById("descripcion");
  const contador = document.getElementById("contador_descripcion");
  if (textarea && contador) {
    contador.textContent = `${textarea.value.length} / 800 caracteres`;
  }
}


/* ──────────────────────────────────────────────────────
   10. FORMULARIO
   ────────────────────────────────────────────────────── */
function manejar_envio_formulario(evento) {
  evento.preventDefault();
  const numero_radicado = `RV-${Date.now().toString().slice(-6)}`;
  const modal = document.getElementById("modal_confirmacion");
  const el_radicado = document.getElementById("numero_radicado");
  if (modal) modal.classList.remove("modal_confirmacion--oculto");
  if (el_radicado) el_radicado.textContent = numero_radicado;
}

function cerrar_confirmacion() {
  const modal = document.getElementById("modal_confirmacion");
  if (modal) modal.classList.add("modal_confirmacion--oculto");
  
  // Reiniciar formulario
  const formulario = document.getElementById("formulario_publicacion");
  if (formulario) formulario.reset();
  
  // Limpiar estado
  estado.fotos_seleccionadas = [];
  estado.amenidades_seleccionadas = new Set(["Piscina", "Gimnasio", "Garaje"]);
  estado.contadores = { habitaciones: 3, banos: 2, parqueaderos: 1, piso: 1 };

  // Ocultar campos condicionales al resetear
  const contenedor_localidad = document.getElementById("contenedor_localidad");
  const contenedor_tipo_zona_medellin = document.getElementById("contenedor_tipo_zona_medellin");
  const contenedor_zona_medellin = document.getElementById("contenedor_zona_medellin");

  if (contenedor_localidad) contenedor_localidad.style.display = "none";
  if (contenedor_tipo_zona_medellin) contenedor_tipo_zona_medellin.style.display = "none";
  if (contenedor_zona_medellin) contenedor_zona_medellin.style.display = "none";

  renderizar_galeria_fotos();
  renderizar_amenidades();
  ["habitaciones", "banios", "parqueaderos", "piso"].forEach(actualizar_visualizacion_contador);
}


/* ──────────────────────────────────────────────────────
   11. INICIALIZACIÓN
   ────────────────────────────────────────────────────── */
function inicializar() {
  // Formulario principal
  const formulario = document.getElementById("formulario_publicacion");
  if (formulario) {
    formulario.addEventListener("submit", manejar_envio_formulario);
  }

  // Select de ciudad para manejar localidades
  const select_ciudad = document.getElementById("ciudad");
  if (select_ciudad) {
    select_ciudad.addEventListener("change", manejar_cambio_ciudad);
  }

  // Select de tipo de zona Medellín
  const select_tipo_zona_medellin = document.getElementById("tipo_zona_medellin");
  if (select_tipo_zona_medellin) {
    select_tipo_zona_medellin.addEventListener("change", manejar_cambio_tipo_zona_medellin);
  }

  // Textarea descripción
  const textarea_desc = document.getElementById("descripcion");
  if (textarea_desc) {
    textarea_desc.addEventListener("input", actualizar_contador_descripcion);
  }

  // Zona de carga de fotos
  const zona_carga = document.getElementById("zona_arrastre");
  if (zona_carga) {
    zona_carga.addEventListener("click", () => document.getElementById("input_fotos").click());
    zona_carga.addEventListener("dragover", (e) => manejar_arrastre(e, true));
    zona_carga.addEventListener("dragleave", (e) => manejar_arrastre(e, false));
    zona_carga.addEventListener("drop", manejar_soltar_archivos);
  }

  // Input de fotos oculto
  const input_fotos = document.getElementById("input_fotos");
  if (input_fotos) {
    input_fotos.addEventListener("change", agregar_fotos_seleccionadas);
  }

  // Inicializar renders de UI dinámica
  renderizar_amenidades();
  ["habitaciones", "banios", "parqueaderos", "piso"].forEach(actualizar_visualizacion_contador);
}

// Arrancar cuando el DOM esté listo
if (document.readyState === "loading") {
  document.addEventListener("DOMContentLoaded", inicializar);
} else {
  inicializar();
}