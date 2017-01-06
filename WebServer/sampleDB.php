<?php
$servername = '10.25.71.66';
$username = 'dbu309yt01';
$password = 'ZuuYea5cBtZ';
$dbname = 'db309yt01';

$conn = mysqli_connect($servername, $username, $password, $dbname);
if(!$conn) {
	die("Connection failed: " . mysqli_connect_error());
}

//$json = "{\"name\": {\"title\": "example",\"name\": \"Stuff\"}}";
$json = "{\"name\":\"Nikolai\",\"ip\":\"10.65.211.228\"}";
//var_dump($json);
var_dump(json_decode($json, true));

if($_GET['func']){
	$sql = "select * from Nick_Test";

	$result =  $conn->query($sql)->fetch_assoc();
	echo "name: " . $result['Name'] . " ip: " . $result['IP'];
	//} else {
	//	echo "Error: " . $sql . "<br>" . mysqli_error($conn);
	//}

}

mysqli_close($conn);

?>
