<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "UnityBackend";

$loginuser=$_POST["loginUser"];
$loginpass=$_POST["loginPass"];

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
echo "Connected Succesfully to the server, waiting credentials verification";

$sql = "SELECT password FROM users where username like '" .$loginuser ."'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    if($row["password"]== $loginpass){
        echo "Login Succes.";
    }
    else echo "Wrong credentials ".$row["password"];
  }
}
else {
  echo "Username not valid";
}
$conn->close();
?>