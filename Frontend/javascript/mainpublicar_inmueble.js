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
  contadores: { habitaciones: 3, banos: 2, parqueaderos: 1 },
};


/* ──────────────────────────────────────────────────────
   2. CONFIGURACIÓN
   ────────────────────────────────────────────────────── */
const lista_amenidades = [
  "Piscina", "Gimnasio", "Garaje", "Ascensor", "BBQ", "Terraza",
  "Jardín", "Seguridad", "Portería", "Concierge", "Lavandería",
  "Depósito", "Salón social", "Cancha", "Zona infantil",
];


/* ──────────────────────────────────────────────────────
   3. CONTADORES NUMÉRICOS
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
   5. MAPA
   ────────────────────────────────────────────────────── */
function abrir_mapa() {
  const ciudad = document.getElementById("ciudad")?.value || "";
  const direccion = document.getElementById("direccion")?.value || "";
  const consulta = encodeURIComponent(`${ciudad} ${direccion}`.trim() || "Colombia");
  window.open(`https://www.google.com/maps/search/${consulta}`, "_blank", "noopener");
}


/* ──────────────────────────────────────────────────────
   6. FOTOGRAFÍAS
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
   7. DESCRIPCIÓN
   ────────────────────────────────────────────────────── */
function actualizar_contador_descripcion() {
  const textarea = document.getElementById("descripcion");
  const contador = document.getElementById("contador_descripcion");
  if (textarea && contador) {
    contador.textContent = `${textarea.value.length} / 800 caracteres`;
  }
}


/* ──────────────────────────────────────────────────────
   8. FORMULARIO
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
  estado.contadores = { habitaciones: 3, banos: 2, parqueaderos: 1 };
  
  renderizar_galeria_fotos();
  renderizar_amenidades();
  ["habitaciones", "banios", "parqueaderos"].forEach(actualizar_visualizacion_contador);
}


/* ──────────────────────────────────────────────────────
   9. INICIALIZACIÓN
   ────────────────────────────────────────────────────── */
function inicializar() {
  // Formulario principal
  const formulario = document.getElementById("formulario_publicacion");
  if (formulario) {
    formulario.addEventListener("submit", manejar_envio_formulario);
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
  ["habitaciones", "banios", "parqueaderos"].forEach(actualizar_visualizacion_contador);
}

// Arrancar cuando el DOM esté listo
if (document.readyState === "loading") {
  document.addEventListener("DOMContentLoaded", inicializar);
} else {
  inicializar();
}