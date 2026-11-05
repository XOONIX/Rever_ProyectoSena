/* ═══════════════════════════════════════════════════════
   main.js — Rever Plataforma Inmobiliaria
   Convención: variables y funciones en español, snake_case
   ═══════════════════════════════════════════════════════ */

/* ──────────────────────────────────────────────────────
    TEXTOS / CONFIG
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
   CAMBIO DE PANTALLA
   ────────────────────────────────────────────────────── */
