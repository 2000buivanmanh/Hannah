<?php 
session_start();

if(isset($_REQUEST['code']))
{
    echo json_encode($_REQUEST['code'] == $_SESSION['verify']);
}
else
{
    echo 0; // no code
}
 ?>