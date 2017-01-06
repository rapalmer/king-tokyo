<?php
$servername = '10.25.71.66';
$username = 'dbu309yt01';
$password = 'ZuuYea5cBtZ';
$dbname = 'db309yt01';

//Opens new Mysql connection for function use
$conn = mysqli_connect($servername, $username, $password, $dbname);
if(!$conn) {
	die("Connection failed: " . mysqli_connect_error());
}


//var_dump("hi");
//$json = json_encode('{"name":"TestHost","ip":"10.65.211.228","character":""}');
//var_dump('{"name":"TestHost","ip":"10.65.211.228","character":""}');
//var_dump($json);
//$sql = "INSERT INTO Server_List (hostname, hostip, playerDetails, status) VALUES ('testname', 'testip', '{name:', 'Creating')";
/*
//Lists all entries in the table
        if($conn->query($sql) === TRUE){
if($_POST['COMMAND'] == "ListPlayers"){
	$sql = "select * from Nick_Test";
        $result =  $conn->query($sql);

        if ($result->num_rows > 0) {
                while($row = $result->fetch_assoc()) {
                	echo "Name: " . $row["name"]. " - IP: " . $row["ip"] . "\r\n";
                }
        } else {
                echo "0 results\r\n";
        }
}

//adds a new entry to the table
if($_POST['COMMAND'] == "NewPlayer"){
	$name = $_POST['name'];
	$ip = $_POST['ip'];
	$sql = "INSERT INTO Nick_Test (name, ip) VALUES ('$name', '$ip')";
	if($conn->query($sql) === TRUE){
		echo "Successfully created record\r\n";
	} else {
		echo "Error creating record\r\n";
	}
}

//deletes an entry in the table
if($_POST['COMMAND'] == "DelPlayer"){
	$name = $_POST['name'];
	$ip = $_POST['ip'];
	$sql = "DELETE FROM Nick_Test WHERE name='$name' AND ip='$ip'";
	if($conn->query($sql) === TRUE){
		echo "Successfully deleted record\r\n";
	} else {
		echo "Error deleting record\r\n";
	}
}*/

if($_POST['COMMAND'] == "login"){
	$name = $_POST['name'];
	$pass = $_POST['pass'];
	$sql = "SELECT * from Users where username='$name' and password='$pass'";
        $result =  $conn->query($sql);
	if ($result->num_rows > 0) {
                while($row = $result->fetch_assoc()) {
                        echo "Name: " . $row["username"]. " - Details: " . $row["details"] . "\r\n";
                }
        } else {
                echo "INVALID\r\n";
        }

}

if($_POST['COMMAND'] == "createUser"){
        $name = $_POST['name'];
        $pass = $_POST['pass'];
	
	$sql = "SELECT * from Users where username='$name'";
	$result = $conn->query($sql);
	if($result->num_rows > 0){
		//Username already taken
		echo "INVALID";
	} else {
        	$sql = "INSERT INTO Users (username, password) VALUES ('$name', '$pass')";
        	if($conn->query($sql) === TRUE){
        	        echo "Successfully created record\r\n";
        	} else {
        	        echo "Error creating record\r\n";
        	}
	}
}

if($_POST['COMMAND'] == "addServer"){
        $hostname = $_POST['hostname'];
        $hostip = $_POST['hostip'];
	$playerDetails = $_POST['playerDetails'];

        $sql = "INSERT INTO Server_List (hostname, hostip, playerDetails, status) VALUES ('$hostname', '$hostip', '$playerDetails', 'Creating')";
        if($conn->query($sql) === TRUE){
                echo "Successfully created record\r\n";
        } else {
                echo "Error creating record\r\n";
        }
}

//This command will handle the adding and remocing of players, changing their details, etc
if($_POST['COMMAND'] == "updateServer"){
	if($_POST['ACTION'] == "addPlayer"){
		$newPlayerDetails = $_POST['playerDetails'];
		$hostip = $_POST['hostIP'];
		$hostname = $_POST['hostname'];
		$sql = "SELECT playerDetails FROM Server_List WHERE hostip = '$hostip' AND hostname = '$hostname'";
		$result = $conn->query($sql);
		$players = [];
		if($result->num_rows > 0){
		        //Should only be one row
		        $row = $result->fetch_assoc();
		        $players = $row['playerDetails'];
		        $players = json_decode($players);
		        $players[] = json_decode($newPlayerDetails);
		        $players = json_encode($players);
		
			$sql = "UPDATE Server_List SET playerDetails = '$players' WHERE hostip = '$hostip'";
			if($conn->query($sql) === TRUE){
                		echo "Successfully updated record\r\n";
			} else {
                		echo "Error updating record\r\n";
			}
		} else {
			echo "No server found\r\n";
		}

        }
}

//Lists all servers in the table
if($_POST['COMMAND'] == "listServers"){
	$sql = "select * from Server_List";
        $result =  $conn->query($sql);

	$servers = [];
	$entry = [];

        if ($result->num_rows > 0) {
                while($row = $result->fetch_assoc()) {
			$entry = ["hostname" => $row["hostname"], "hostip" => $row["hostip"], "players" => $row["playerDetails"]];
//	               	echo $row["hostname"]. " - IP: " . $row["hostip"] . "\r\n";
			$servers[] = $entry;
                }
		echo json_encode($servers);
//				echo "\0";
        } else {
                echo "0 results\r\n";
        }
}

//deletes an entry in the table
if($_POST['COMMAND'] == "delServer"){
	$hostname = $_POST['hostname'];
	$hostip = $_POST['hostip'];
	$sql = "DELETE FROM Server_List WHERE hostname='$hostname' AND hostip='$hostip'";
	if($conn->query($sql) === TRUE){
		echo "Successfully deleted server\r\n";
	} else {
		echo "Error deleting server\r\n";
	}
}


mysqli_close($conn);

?>
