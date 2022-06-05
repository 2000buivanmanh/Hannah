<?php
/**
 * The Header for our theme
 *
 * Displays all of the <head> section and everything up till <div id="main">
 *
 * @package WordPress
 * @subpackage Kiddy
 * @since Kiddy 1.0
 */
?><!DOCTYPE html>
<html <?php language_attributes(); ?>>
<head>
	<meta charset="<?php bloginfo( 'charset' ); ?>">
	<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1"> 
	<link rel="profile" href="http://gmpg.org/xfn/11">
	<link rel="pingback" href="<?php bloginfo( 'pingback_url' ); ?>">
	<?php
		$fav = cws_get_option('favicon');
		$fav_url = isset($fav) ? bfi_thumb( $fav['url'], array( 'width' => 32, 'height' => 32, 'crop' => true ) ) : '';
		echo !empty($fav_url) ? '<link rel="shortcut icon" href="'. $fav_url .'" />' : '';
	?>
	<?php
		cws_process_fonts();
		cws_process_colors();
		/***** Google Analytics' *****/
		cws_put_ganalytics_code();
		echo '<script>
								// Loads the IFrame Player API code asynchronously.
  								var tag = document.createElement("script");
  								tag.src = "https://www.youtube.com/player_api";
  								var firstScriptTag = document.getElementsByTagName("script")[0];
  								firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
							</script>';	
		/***** \Google Analytics' *****/		
	?>
	<style id='cws-cp-1'></style>
	<style id='cws-cp-2'></style>
	<style id='cws-cp-3'></style>
	<style id='cws-cp-4'></style>
	<style id='cws-cp-5'></style>
	<?php 
		wp_head();
	?>

</head>

<body <?php body_class(); ?>>

	<?php
		$header_after_slider = false;

		/***** Boxed Layout *****/
		$boxed_layout = ('0' != cws_get_option('boxed-layout') ) ? 'boxed' : '';
		echo '<div class="page'.($boxed_layout ? ' page_boxed' : '').'">';
		/***** \Boxed Layout *****/


		ob_start();
		$text['home']	 = __('Home', 'kiddy' ); // text for the 'Home' link
		$text['category'] = __('Category "%s"', 'kiddy' ); // text for a category page
		$text['search']   = __('Search for "%s"', 'kiddy' ); // text for a search results page
		$text['taxonomy'] = __('Archive by %s "%s"', 'kiddy' );
		$text['tag']	  = __('Posts Tagged "%s"', 'kiddy' ); // text for a tag page
		$text['author']   = __('Articles Posted by %s', 'kiddy' ); // text for an author page
		$text['404']	  = __('Error 404', 'kiddy' ); // text for the 404 page

		$page_title = "";
		if ( is_404() ){
			$page_title = __( "404 Page", 'kiddy' );
		}
		else if ( is_search() ){
			$page_title = __( "Search", 'kiddy' );
		}
		else if ( is_home() ){
			$page_title = __( "Home", 'kiddy' );
		}else if ( is_category() ) {
			$cat = get_category( get_query_var('cat') );
			$cat_name = isset( $cat->name ) ? $cat->name : "";
			$page_title = sprintf( $text['category'], $cat_name ) ;	
		}else if ( is_tag()) {
			$page_title = sprintf( $text['tag'], single_tag_title('', false) ) ;	
		}
		elseif ( is_day() ) {
			echo sprintf($link, get_year_link(get_the_time('Y')), get_the_time('Y')) . $delimiter;
			echo sprintf($link, get_month_link(get_the_time('Y'),get_the_time('m')), get_the_time('F')) . $delimiter;
			$page_title = get_the_time('d');

		} elseif ( is_month() ) {
			$page_title = get_the_time('F');

		} elseif ( is_year() ) {
			$page_title = get_the_time('Y');

		} elseif ( has_post_format() && !is_singular() ) {
			$page_title = get_post_format_string( get_post_format() );

		}
		else if( is_tax( array( 'cws_portfolio_cat', 'cws_staff_member_department', 'cws_staff_member_position' ) ) ){
			$tax_slug = get_query_var( 'taxonomy' );
			$term_slug = get_query_var( $tax_slug );
			$tax_obj = get_taxonomy( $tax_slug );
			$term_obj = get_term_by( 'slug', $term_slug, $tax_slug );

			$singular_tax_label = isset( $tax_obj->labels ) && isset( $tax_obj->labels->singular_name ) ? $tax_obj->labels->singular_name : "";
			$term_name = isset( $term_obj->name ) ? $term_obj->name : "";
			$page_title = $singular_tax_label . " " . $term_name ;	
		}
		elseif ( is_archive() ){
			$post_type = get_post_type();
			$post_type_obj = get_post_type_object( $post_type );
			$post_type_name = isset( $post_type_obj->label ) ? $post_type_obj->label : "";
			$page_title = $post_type_name ;
		}
		else if(is_woo()){
			$page_title = woocommerce_page_title(false);	
		}
		else{
			$page_title = get_the_title();
		}
		


		if ( function_exists('yoast_breadcrumb') ) {
  			$breadcrumbs = yoast_breadcrumb( "<nav class='bread-crumbs'>", "</nav>", false );
  		} else{
			ob_start();
			dimox_breadcrumbs();
			$breadcrumbs = ob_get_clean();
  		}

		$use_breadcrumbs = cws_get_option('breadcrumbs') == 1 ? true : false;

		$use_wave_breadcrumbs = cws_get_option('wave-style') == 1 ? true : false;

		if ( !empty( $page_title ) || (!empty( $breadcrumbs ) && $use_breadcrumbs ) ){
			echo $use_wave_breadcrumbs ? "<section class='page_title wave'>" : "<section class='page_title flat'>";
				echo "<div class='container'>";
					echo !empty( $page_title ) ? "<div class='title'><h1>$page_title</h1></div>" : "";
					echo (!empty( $breadcrumbs ) && $use_breadcrumbs) ? $breadcrumbs : "";
				echo "</div>";
				echo $use_wave_breadcrumbs ? "<canvas class='breadcrumbs' data-bg-color='".cws_get_option('theme-custom-secondary-color')."' data-line-color='".cws_get_option('theme-custom-outline-color')."'></canvas>" : '';
			echo "</section>";
		}
		
		$page_title_content = ob_get_clean();


		ob_start();	
		$is_revslider_active = is_plugin_active( 'revslider/revslider.php' );
		$cws_revslider_content = "";
		$slider_type = "none";
		if ( is_front_page()){
			$slider_type = cws_get_option( 'home-slider-type' );
			switch( $slider_type ){
				case 'img-slider':
					if ( is_page() ){
						$slider_settings = cws_get_page_meta_var( "slider" );
						if ( isset( $slider_settings['slider_override'] ) && $slider_settings['slider_override'] ){
							$slider_options = isset( $slider_settings['slider_options'] ) ? $slider_settings['slider_options'] : array();
						}
						else{
							$slider_options = cws_get_option( 'home-header-slider-options' );
						}
					}
					else if ( is_home() ){
						$slider_options = cws_get_option( 'home-header-slider-options' );
					}
					echo do_shortcode($slider_options);
					break;
				case 'video-slider':
					$video_options = cws_get_option( 'home-header-video-options' );
					$video_options = isset( $video_options['@'] ) ? $video_options['@'] : array();
					extract( shortcode_atts( array(
						'slider_shortcode' => '',
						'slider_switch' => '',
						'video_type' => 'self_hosted',
						'video_header_height' => '',
						'sh_source' => array(),
						'youtube_source' => '',
						'vimeo_source' => '',
						'color_overlay_type' => '',
						'overlay_color' => '',
						'gradient_settings' => array(),
						'color_overlay_opacity' => '40',
						'use_pattern' => '0',
						'pattern_image' => array()
					), $video_options));
					$sh_source = isset( $sh_source['url'] ) && !empty( $sh_source['url'] ) ? $sh_source['url'] : "";
					$color_overlay_opacity = (int)$color_overlay_opacity / 100;
					$has_video_src = false;
					$header_video_atts = "";
					// $header_video_class = "row_bg_video";
					$header_video_class = "fs_video_bg";
					$header_video_styles = "";
					$header_video_html = "";
					$uniqid = uniqid( 'video-' );
					switch ( $video_type ){
						case 'self_hosted':
							if ( !empty( $sh_source ) ){
								$has_video_src = true;
								$header_video_class .= " cws_self_hosted_video";
								$header_video_html .= "<video class='self_hosted_video' src='$sh_source' autoplay='autoplay' loop='loop' muted='muted'></video>";
							}
							break;
						case 'youtube':
							if ( !empty( $youtube_source ) ){
								$has_video_src = true;
								$header_video_class .= " cws_Yt_video_bg loading";
								$header_video_atts .= " data-video-source='$youtube_source' data-video-id='$uniqid'";
								$header_video_html .= "<div id='$uniqid'></div>";								
							}
							break;
						case 'vimeo':
							if ( !empty( $vimeo_source ) ){
								$has_video_src = true;
								$header_video_class .= " cws_Vimeo_video_bg";
								$header_video_atts .= " data-video-source='$vimeo_source' data-video-id='$uniqid'";
								$header_video_html .= "<iframe id='$uniqid' src='" . $vimeo_source . "?api=1&player_id=$uniqid' frameborder='0'></iframe>";
							}
							break;
					}
					if ( $has_video_src ){
						$header_after_slider = true;
					}
					if ( $use_pattern && !empty( $pattern_image ) && isset( $pattern_image['url'] ) && !empty( $pattern_image['url'] ) ){
						$pattern_img_src = $pattern_image['url'];
						$header_video_html .= "<div class='bg_layer' style='background-image:url(" . $pattern_img_src . ")'></div>";
					}
					if ( !empty( $overlay_color ) ){
						$header_video_html .= "<div class='bg_layer' style='background-color:" . $overlay_color . ";" . ( !empty( $color_overlay_opacity ) ? "opacity:$color_overlay_opacity;" : "" ) . "'></div>";
					}

					$header_video_atts .= !empty( $header_video_class ) ? " class='" . trim( $header_video_class ) . "'" : "";
					$header_video_atts .= !empty( $header_video_styles ) ? " style='$header_video_styles'" : "";

					if (!empty( $slider_shortcode ) && $has_video_src && $slider_switch == 1) {
						echo "<div class='fs_video_slider'>";
							if ( $is_revslider_active ){
								echo  do_shortcode( $slider_shortcode ) ;
							}
							else{
								echo do_shortcode( "[cws_sc_msg_box type='warning' is_closable='1' text='Install and activate Slider Revolution plugin'][/cws_sc_msg_box]" );
							}
							echo "<div " . $header_video_atts . ">";
								echo $header_video_html;
							echo "</div>";
							$header_after_slider = true;
						echo "</div>";
					}elseif ($has_video_src && $slider_switch == 0) {
						$video_height_coef = 960/$video_header_height;
						echo "<div class='fs_video_slider' style='height:".$video_header_height."px' data-wrapper-height='".$video_height_coef."'>";
							echo "<div " . $header_video_atts . ">";
								echo $header_video_html;
							echo "</div>";
						echo "</div>";
						$header_after_slider = true;
					}elseif(!empty( $slider_shortcode ) && $slider_switch == 1 && !$has_video_src){
						if ( $is_revslider_active ){
							echo  do_shortcode( $slider_shortcode ) ;
						}
						else{
							echo do_shortcode( "[cws_sc_msg_box type='warning' is_closable='1' text='Install and activate Slider Revolution plugin'][/cws_sc_msg_box]" );
						}
						$header_after_slider = true;
					}

 					break;
				case 'stat-img-slider':
					$image_options = cws_get_option("home-header-image-options");
					$has_slider_img = !empty( $image_options['url'] ) ? true : false;
					$img_stat_height = cws_get_option("static_image_height"); 
					$img_stat_height_coef = 960/$img_stat_height;

					if ( $has_slider_img ){
						$img_src = $image_options['url'];
						echo "
								<div class='image_stat_header' style='height:".$img_stat_height."px' data-wrapper-height='".$img_stat_height_coef."'>
									<img src='$img_src' alt />
								</div>";
					}
					break;
			}
		}
		else if ( is_page() ){
			$slider_settings = cws_get_page_meta_var( "slider" );
			if ( $slider_settings['slider_override'] ){
				$slider_options = $slider_settings['slider_options']['@']['shortcode'];
				echo do_shortcode($slider_options);
			}
		}
		$slider_content = ob_get_clean();

		ob_start();
			is_plugin_active('woocommerce/woocommerce.php') ? woocommerce_mini_cart() : '';
		$woo_mini_cart = ob_get_clean();

		ob_start();
		if (is_plugin_active('woocommerce/woocommerce.php')) {
			?>
				<a class="woo_icon" href="<?php echo WC()->cart->get_cart_url(); ?>" title="<?php _e( 'View your shopping cart', 'kiddy' ); ?>"><i class='woo_mini-count fa fa-shopping-cart'><?php echo ((WC()->cart->cart_contents_count > 0) ?  "<span>" . WC()->cart->cart_contents_count ."</span>" : '') ?></i></a>
			<?php
		}
		$woo_mini_icon = ob_get_clean();

		ob_start();
		$social_links_location = cws_get_option( 'social_links_location' );
		$top_panel_text = cws_get_option( 'top_panel_text' );
		$use_wave = cws_get_option( "wave-style" ) == 1 ? true : false;
		$top_panel_slider = cws_get_option( 'top_panel_slider' ) == 1 ? true : false;
		$social_links = "";
		$show_wpml_header = cws_is_wpml_active() ? true : false;

		if ( in_array( $social_links_location, array( 'top', 'top_bottom' ) ) ){
			$social_links = render_cws_social_links();	
		}
		if ( !empty( $social_links ) || !empty( $top_panel_text )){
			echo "<div class='site_top_panel ".($use_wave ? 'wave' : '')." ".($top_panel_slider ? 'slider' : '')."'>";
				echo $use_wave ? "<div class='top_half_sin_wrapper'><canvas class='top_half_sin' data-bg-color='#ffffff' data-line-color='#ffffff'></canvas></div>" : '';
				echo "<div class='container'>";
				echo "<div class='row_text_search'>";
					echo !empty( $top_panel_text ) ? "<div id='top_panel_text'>$top_panel_text</div>" : "";
					get_search_form();
				echo "</div>";
					echo "<div id='top_panel_links'>";
						echo "<div class='search_icon'></div>";
						echo !empty( $social_links ) ? "<div id='top_social_links_wrapper'><div class='share-toggle-button'><i class='share-icon fa fa-share-alt'></i></div>$social_links</div>" : "";
						
						echo is_plugin_active('woocommerce/woocommerce.php') ? "<div class='mini-cart'>$woo_mini_icon$woo_mini_cart</div>" : '';

						if($show_wpml_header) : ?>
							<div class="lang_bar">
								<?php do_action('icl_language_selector'); ?>
				 		 	</div>
						<?php 	endif; 

					echo "</div>";
					echo $top_panel_slider ? '<div class="site_top_panel_toggle"></div>' : '';
				echo "</div>";
			echo "</div>";
		}
		$top_panel_content = ob_get_clean();





	/***** Logo Settings *****/	
	ob_start();
	$logo = cws_get_option('logo');
	$logo_is_high_dpi = cws_get_option('logo_is_high_dpi');
	if (isset($logo["url"])) {
		$logo_border = cws_get_option('logo-with-border');
		$logo_hw = cws_get_option('logo-dimensions');
		$logo_m = cws_get_option('logo-margin');
		$bfi_args = array();
		if (is_array($logo_hw)){
			foreach ($logo_hw as $key => $value) {
				if ( !empty($value) ){
					$bfi_args[$key] = $value;
					$bfi_args['crop'] = true;
				}
			}
		}
		$logo_lr_spacing = "";
		$logo_tb_spacing = "";
		$logo_src = "";
		if (is_array($logo_m)){
			$logo_lr_spacing .= ( !empty($logo_m["margin-left"]) ? "margin-left:" . $logo_m["margin-left"] . "px;" : "" ) . ( !empty($logo_m["margin-right"]) ? "margin-right:" . $logo_m["margin-right"] . "px;" : "" ) . ( !empty($logo_m["margin-top"]) ? "margin-top:" . $logo_m["margin-top"] . "px;" : "" ) . ( !empty($logo_m["margin-bottom"]) ? "margin-bottom:" . $logo_m["margin-bottom"] . "px;" : "" );
			$logo_tb_spacing .= ( !empty($logo_m["margin-top"]) ? "padding-top:" . $logo_m["margin-top"] . "px;" : "" ) . ( !empty($logo_m["margin-bottom"]) ? "padding-bottom:" . $logo_m["margin-bottom"] . "px;" : "" );
		}

		if ( isset( $logo["url"] ) && ( !empty( $logo["url"] ) ) ) {
			if ( empty( $bfi_args ) ){
				if ( $logo_is_high_dpi ){
					$thumb_obj = bfi_thumb($logo["url"],array( 'width' => floor((int)$logo['width'] / 2),'crop' => true ),false);
					$thumb_path_hdpi = $thumb_obj[3]['retina_thumb_exists'] ? " src='". $thumb_obj[0] ."' data-at2x='" . $thumb_obj[3]['retina_thumb_url'] ."'" : " src='". $thumb_obj[0] . "' data-no-retina";
					$logo_src = $thumb_path_hdpi;
				}
				else{
					$logo_src = " src='".$logo["url"]."' data-no-retina";
				}
			}
			else{
				$thumb_obj = bfi_thumb($logo["url"],$bfi_args,false);
				$thumb_path_hdpi = $thumb_obj[3]['retina_thumb_exists'] ? " src='". $thumb_obj[0] ."' data-at2x='" . $thumb_obj[3]['retina_thumb_url'] ."'" : " src='". $thumb_obj[0] . "' data-no-retina";
				$logo_src = $thumb_path_hdpi;
			}
		}
	}
	/***** \Logo Settings *****/

	$logo_sticky = cws_get_option('logo_sticky');
	$logo_sticky_src = '';
	$logo_sticky_is_high_dpi = cws_get_option('logo_sticky_is_high_dpi');
	if ( isset( $logo_sticky["url"] ) && ( !empty( $logo_sticky["url"] ) ) ) {
		$logo_sticky_src = "";
		if ( $logo_sticky_is_high_dpi ){
			$thumb_obj = bfi_thumb($logo_sticky["url"],array( 'width' => floor((int)$logo_sticky['width'] / 2),'crop' => true ),false);
			$thumb_path_hdpi = $thumb_obj[3]['retina_thumb_exists'] ? " src='". $thumb_obj[0] ."' data-at2x='" . $thumb_obj[3]['retina_thumb_url'] ."'" : " src='". $thumb_obj[0] . "' data-no-retina";
			$logo_sticky_src = $thumb_path_hdpi;
		}
		else{
			$logo_sticky_src = " src='".$logo_sticky["url"]."' data-no-retina";
		}
	}

	$logo_mobile = cws_get_option('logo_mobile');
	$logo_mobile_src = '';
	$logo_mobile_is_high_dpi = cws_get_option('logo_mobile_is_high_dpi');
	if ( isset( $logo_mobile["url"] ) && ( !empty( $logo_mobile["url"] ) ) ) {
		$logo_mobile_src = "";
		if ( $logo_mobile_is_high_dpi ){
			$thumb_obj = bfi_thumb($logo_mobile["url"],array( 'width' => floor((int)$logo_mobile['width'] / 2),'crop' => true ),false);
			$thumb_path_hdpi = $thumb_obj[3]['retina_thumb_exists'] ? " src='". $thumb_obj[0] ."' data-at2x='" . $thumb_obj[3]['retina_thumb_url'] ."'" : " src='". $thumb_obj[0] . "' data-no-retina";
			$logo_mobile_src = $thumb_path_hdpi;

		}
		else{
			$logo_mobile_src = " src='".$logo_mobile["url"]."' data-no-retina";
		}
	}




	/***** Logo Position *****/
	$logo_position = cws_get_option('logo-position');
	$header_class = 'site_header';
	if ( isset($logo_position) && !empty($logo_position) ){
		$header_class .= ' logo-' . $logo_position;
	}
	if ( isset($logo_sticky_src ) && !empty($logo_sticky_src ) ){
		$header_class .= ' custom_sticky_logo';
	}
	if ( isset($logo_mobile_src ) && !empty($logo_mobile_src ) ){
		$header_class .= ' custom_mobile_logo';
	}
	/***** \Logo Position *****/

	/***** Menu Position *****/
	global $current_user;
	$menu_locations = get_nav_menu_locations();
	$menu_position = cws_get_option("menu-position");
	$customize_headers = cws_get_option('customize_headers');
	$header_mask_color = cws_get_option('theme-custom-color');
	$header_mask_image = cws_get_option("header_img");
	$header_mask_image_url = ($customize_headers == 1) ? $header_mask_image["url"] : '';
	$header_mask_pattern = cws_get_option("header_pattern");
	$header_mask_pattern_url = ($customize_headers == 1) ? $header_mask_pattern["url"] : "" ;
	$show_header_outside_slider = cws_get_option("show_header_outside_slider");
	$header_outsid_slider = cws_get_option("show_header_outside_slider");

	$has_img = false;

	if ( has_post_thumbnail() && !is_woo() && !is_single() && !(get_post_type() == 'post') && !(get_post_type()=='cws_staff') && !(get_post_type()=='cws_portfolio')){
		$img_object = wp_get_attachment_image_src( get_post_thumbnail_id(), 'full' );
		$img_url = isset( $img_object[0] ) ? $img_object[0] : "";
		$has_img = true;
	}
	else if ( isset( $header_mask_image_url ) && is_array( $header_mask_image ) && !empty( $header_mask_image_url ) ){
		$img_url = $header_mask_image_url;
		$has_img = true;
	}

	ob_start();

	if (($has_img || $header_mask_color || $header_mask_pattern_url) ) {
		echo "<div class='header_mask'".($header_mask_color ? "style='background-color:$header_mask_color;'": "") .">";
			echo $header_mask_pattern_url ? "<div class='header_pattern' style='background-image:url($header_mask_pattern_url);'></div>" :'';
			if ( empty($slider_content) || (!($show_header_outside_slider == 1) && !empty($slider_content))) {
				echo $has_img ? "<div class='header_img' style='background-image:url($img_url);'></div>" : "";
			}
			
		echo "</div>";
	}

	$header_mask = ob_get_clean();

	/***** \Menu Position *****/	
	?>

	<?php 
		echo !empty($header_mask) ? "<div class='header_cont'>" : '';
		echo $header_mask;
		$header_style = '';
		$customize_headers = cws_get_option('customize_headers');
		$header_top_margin = cws_get_option('header_top_margin'); 
		$header_bot_margin = cws_get_option('header_bot_margin'); 
		$header_style .= (($customize_headers ==1) && !empty($header_top_margin))  ? 'padding-top: '.$header_top_margin.'px;' : '' ;
		$header_style .= (($customize_headers ==1) && !empty($header_bot_margin))  ? 'padding-bottom: '.$header_bot_margin.'px;' : '' ;
	?>

	<header <?php echo !empty($header_class) ? "class='$header_class'" : ""; ?>>
		
		<div class="header_box" <?php echo !empty($header_style) ? ' style="'.$header_style.'"' : ''; ?>>
			<div class="container">
				<?php
				if ( !empty($logo["url"]) && $logo_position !== "in-menu"){
					?>
					<div class="header_logo_part<?php echo $logo_border>0 ? ' with_border' : '' ?>" role="banner" <?php echo isset($logo_position) && !empty($logo_position) && $logo_position == "center" && !empty($logo_tb_spacing) ? " style='$logo_tb_spacing'" : ""; ?>>
						<a <?php echo (!empty($logo_lr_spacing) ? " style='$logo_lr_spacing'" : "") ?> class="logo" href="<?php echo home_url(); ?>" ><?php echo $logo_sticky_src ?  "<img ".$logo_sticky_src." class='logo_sticky' alt />" : "";?><?php echo $logo_mobile_src ?  "<img ".$logo_mobile_src." class='logo_mobile' alt />" : "";?><img <?php echo $logo_src; ?> alt /></a>
					</div>
					<?php
				}
				if( isset($menu_locations['header-menu']) ) {
					?>
					<div class="header_nav_part">
						<nav class="main-nav-container <?php echo !empty($menu_position) ? 'a-' . $menu_position : ''; ?>">
							<div class="mobile_menu_header">
								<i class="mobile_menu_switcher"><span></span><span></span><span></span></i>
							</div>
							<?php
								$bees_menu = cws_get_option('menu-with-bees') == 1 ? ' menu-bees' : '';
								wp_nav_menu( array(
									'theme_location'  => 'header-menu',
									'menu_class' => 'main-menu'.$bees_menu,
									'container' => false,
									'walker' => new Cws_Walker_Nav_Menu()
								) );
							?>
						</nav>
					</div>
					<?php
				}
				?>
			</div>
		</div>
	</header><!-- #masthead -->
	<?php
		echo !empty($header_mask) ? "</div>" : '';
		$header_content = ob_get_clean();
	?>
	<?php
		$benefit_patt = cws_get_option('benefits_pattern');
		$benefit_patt_url = !empty($benefit_patt['url']) ? $benefit_patt['url'] : '';
		$benefit_patt_style = (!empty($benefit_patt_url) && !$use_wave) ? " style='background-image: url(".$benefit_patt_url.")'" : '';
		ob_start();
		$benefits_sidebar = cws_get_page_meta_var( array( 'benefits', 'benefits_sb' ) );
		if ( is_front_page() && empty($benefits_sidebar)){
			$benefits_area_sidebar = cws_get_option( 'benefits-sidebar' );
			if ( !empty( $benefits_area_sidebar ) && is_active_sidebar( $benefits_area_sidebar ) ){
				echo "<div class='benefits_area".($use_wave ? ' wave' : '')."'$benefit_patt_style>";
					echo $use_wave_breadcrumbs ? "<div class='cloud_wrapper'><canvas class='cloud' data-bg-color='".cws_get_option('theme-custom-outline-color')."' data-line-color='#ffffff' ".(!empty($benefit_patt['url']) ? "data-pattern-src='".$benefit_patt_url."'" : '')."'></canvas></div>" : '';
					echo "<div class='container'>";
						echo "<div class='benefits_container'>";
							$GLOBALS['benefits_area_is_rendered'] = true;
							dynamic_sidebar( $benefits_area_sidebar );
							unset( $GLOBALS['benefits_area_is_rendered'] );
						echo "</div>";
					echo "</div>";
				echo "</div>";
			}
		}else if(is_page()){
			$benefits_area_sidebar = $benefits_sidebar;
			if ( !empty( $benefits_area_sidebar ) && is_active_sidebar( $benefits_area_sidebar ) ){
				echo "<div class='benefits_area".($use_wave ? ' wave' : '')."'$benefit_patt_style>";
					echo $use_wave_breadcrumbs ? "<div class='cloud_wrapper'><canvas class='cloud' data-bg-color='".cws_get_option('theme-custom-outline-color')."' data-line-color='#ffffff' ".(!empty($benefit_patt['url']) ? "data-pattern-src='".$benefit_patt_url."'" : '')."'></canvas></div>" : '';
					echo "<div class='container'>";
						echo "<div class='benefits_container'>";
							$GLOBALS['benefits_area_is_rendered'] = true;
							dynamic_sidebar( $benefits_area_sidebar );
							unset( $GLOBALS['benefits_area_is_rendered'] );
						echo "</div>";
					echo "</div>";
				echo "</div>";
			}

		}
		$benefits_area_content = ob_get_clean();

		echo $top_panel_content;
		echo (($show_header_outside_slider == 1) && !empty($slider_content)) ? "<div class='slider_vs_menu'>" : "";
		echo $header_content;
		echo $slider_content;
		echo (($show_header_outside_slider == 1) && !empty($slider_content)) ? "</div>" : "";
		echo $benefits_area_content;
		echo (!( is_front_page() ) && empty($slider_content) && empty($benefits_area_content)) ? $page_title_content : ( (( ( is_front_page() || !empty($benefits_area_content) ) && (!is_front_page() && !empty($benefits_area_content))) || (!$use_wave_breadcrumbs)) ? '' : '<div class="cloud_wrapper"><canvas class="white_cloud"></canvas></div>');
	?>

	<div id="main" class="site-main">
