<?php 
	$servername = "localhost";
	$username = "u501064479_storm";
	$pass = "E08lz5u7V";
	$dbname ="u501064479_blhs";

	if(isset( $_POST['serviceid']) ){
        $serviceid =  $_POST['serviceid'];
          if($serviceid=="33"&&isset($_POST['player']))
            {
            $playername=$_POST['player'];


             $connect = new mysqli($servername,$username,$pass,$dbname);

	if ($conn->connect_error) {
                die("Connection failed: " . $conn->connect_error);
	                           } 
                               
	else {
	$query1 = "INSERT INTO `Highscore`(`IDENTITY`, `Name`, `Highscores`) VALUES (null,'$playername','0')";
        $connect->query($query1);

	$query = "SELECT MAX( IDENTITY ) AS lastid
FROM Highscore";
       $result = $connect->query($query);
         
       if ($result->num_rows > 0) {
	while ($row = $result->fetch_assoc()) {
        echo $row['lastid'];
                                                }
	                             }
	else echo "sem resultados /n";
	       }


              }
          }
         if ($serviceid =="392"){

	$connect = new mysqli($servername,$username,$pass,$dbname);
	if ($conn->connect_error) {
               die("Connection failed: " . $conn->connect_error);
	                } 
	else {
	
	$query = "SELECT * FROM  `Highscore` ORDER BY Highscores DESC LIMIT 12";
	$result = $connect->query($query);

	if ($result->num_rows > 0) {
	while ($row = $result->fetch_assoc()) {
        echo $row["Name"]. "|". $row["Highscores"]. " " ;
                                                }
	                             }
	else echo "sem resultados /n";
	       }
                                    }

	
	?>										