<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "UnityBackend";

$loginuser=$_POST["loginUser"];

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
//echo "Connected Succesfully to the server, waiting credentials verification";

$sql = "SELECT username,score FROM users where username like '" .$loginuser ."'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    if($row["username"]== $loginuser){
        echo $row["score"];
    }
  }
}
else {
  echo "Error";
}
$conn->close();
?>