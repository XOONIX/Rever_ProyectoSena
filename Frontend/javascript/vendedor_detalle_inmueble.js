 function setStatus(status) {
        // Remover clase activa de todos
        document.querySelectorAll('.status-option').forEach(opt => {
            opt.classList.remove('active');
        });
        
        // Agregar clase activa al seleccionado
        const selectedOpt = document.querySelector(`.status-option[data-status="${status}"]`);
        if (selectedOpt) {
            selectedOpt.classList.add('active');
        }
        
        // Actualizar el valor del input oculto
        document.getElementById('propertyStatus').value = status;
    }

    // Activar/desactivar campos de tiempo según el checkbox del día
    document.querySelectorAll('.schedule-row input[type="checkbox"]').forEach(checkbox => {
        checkbox.addEventListener('change', function() {
            const row = this.closest('.schedule-row');
            const timeInputs = row.querySelectorAll('input[type="time"]');
            timeInputs.forEach(input => {
                input.disabled = !this.checked;
            });
        });
    });

    // Manejo del envío del formulario (ejemplo)
    document.getElementById('editPropertyForm').addEventListener('submit', function(e) {
        e.preventDefault();
        const formData = new FormData(this);
        alert('¡Cambios guardados con éxito! Estado actual: ' + formData.get('status').toUpperCase());
        // Aquí integrarías el fetch/axios hacia tu API backend.
    });