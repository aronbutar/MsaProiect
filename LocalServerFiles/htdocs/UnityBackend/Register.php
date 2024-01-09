<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "UnityBackend";

$loginuser=$_POST["loginUser"];
$loginemail=$_POST["loginEmail"];
$loginpass=$_POST["loginPass"];

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
//echo "Connected Succesfully to the server, waiting credentials verification";

$sql = "SELECT username,email FROM users where username like '" .$loginuser ."' or email like '".$loginemail ."'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    echo "Account already exists ";
}
else {
  //echo "Creating User";
  $sql= "INSERT INTO users (username,password,email,score) VALUES ('".$loginuser ."','".$loginpass ."','".$loginemail ."',0)";
  if ($conn->query($sql) === TRUE) {
    echo "New Account created successfully";
  } else {
    echo "Error: " . $sql . "<br>" . $conn->error;
  }
}
$conn->close();
?>