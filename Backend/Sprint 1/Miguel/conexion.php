<?php
error_reporting(E_ALL);
ini_set('display_errors', 1);

$conexion = new mysqli("127.0.0.1", "root", "", "rever_inmobiliaria_v2", 3306);

if ($conexion->connect_error) {
    die("Error de conexión: " . $conexion->connect_error);
    echo "Conexión exitosa";
}
?>