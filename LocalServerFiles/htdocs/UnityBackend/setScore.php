<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "UnityBackend";

$loginuser=$_POST["loginUser"];
$loginscore=$_POST["loginScore"];

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
//echo "Connected Succesfully to the server, waiting credentials verification";
$querry= "UPDATE users set score='".$loginscore."' where username like '" .$loginuser ."'" ;
//$sql = "SELECT * FROM users where username like '" .$loginuser ."'";
mysqli_query($conn,$querry);


$conn->close();
?>