<?php
	/* Attach tunner scripts file */
	add_action('wp_enqueue_scripts', 'cws_child_theme_enqueue_scripts');
	function cws_child_theme_enqueue_scripts(){
		wp_register_script( 'clrpicker', get_stylesheet_directory_uri() . '/tuner/js/colorpicker.js' );
		wp_enqueue_script('clrpicker');
		wp_register_script( 'tuner', get_stylesheet_directory_uri() . '/tuner/js/scripts.js' );
		wp_enqueue_script('tuner');	
	}

?>