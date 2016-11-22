<?php 
	$servername = "sql3.freemysqlhosting.net";
	$username = "sql3145658";
	$pass = "zhcS585b8Y";
	$dbname ="sql3145658";
	
	$connect = new mysqli($servername,$username,$pass,$dbname);
	if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
	} 
	else {
	
	$query = "SELECT * FROM  `Highscore` ORDER BY Highscore DESC";
	$result = $connect->query($query);

	if ($result->num_rows > 0) {
	while ($row = $result->fetch_assoc()) {
        echo $row["Name"]. " ". $row["Highscore"].  "\n";
    }
	}
	else echo "Não há resultado /n";
	}
	?>