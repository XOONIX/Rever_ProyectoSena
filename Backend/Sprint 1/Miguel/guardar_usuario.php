<?php
include("conexion.php");

if ($_SERVER['REQUEST_METHOD'] == 'POST') {

    $nombre = $_POST['nombre'];
    $correo = $_POST['correo'];
    $telefono = $_POST['telefono'];
    $id_rol = $_POST['id_rol'];

    // Validar contraseña
    if (!isset($_POST['contrasena'])) {
        die("Error: no se recibió la contraseña");
    }

    $contrasena = password_hash($_POST['contrasena'], PASSWORD_DEFAULT);

    // Validar correo duplicado
    $verificar = $conexion->query("SELECT * FROM usuarios WHERE correo = '$correo'");
    if ($verificar->num_rows > 0) {
        die("Este correo ya está registrado");
    }

    // Insertar
    $sql = "INSERT INTO usuarios 
    (nombre, correo, contrasena, telefono, id_rol)
    VALUES 
    ('$nombre', '$correo', '$contrasena', '$telefono', '$id_rol')";

    if ($conexion->query($sql) === TRUE) {
        echo "Usuario registrado correctamente";
    } else {
        echo "Error: " . $conexion->error;
    }

}
?>