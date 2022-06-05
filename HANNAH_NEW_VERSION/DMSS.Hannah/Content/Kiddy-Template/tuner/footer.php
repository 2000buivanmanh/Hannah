<?php
/**
 * The template for displaying the footer
 *
 * Contains footer content and the closing of the #main and #page div elements.
 *
 * @package WordPress
 * @subpackage Kiddy
 * @since Kiddy 1.0
 */
?>
		<svg xmlns="http://www.w3.org/2000/svg" version="1.1" width="0" style='display:none;'>
		  <defs>
		    <filter id="goo">
		      <feGaussianBlur in="SourceGraphic" stdDeviation="6" result="blur" />
		      <feColorMatrix in="blur" mode="matrix" values="1 0 0 0 0  0 1 0 0 0  0 0 1 0 0  0 0 0 19 -9" result="goo" />
		      <feComposite in="SourceGraphic" in2="goo" operator="atop"/>
		    </filter>
		  </defs>
		</svg>

		</div><!-- #main -->

		<?php
			$footer_pattern = cws_get_option("footer_pattern");
			$footer_sidebar = is_page() ? cws_get_page_meta_var( array( 'footer', 'footer_sb_top' ) ) : cws_get_option( "footer-sidebar-top" );
			$use_wave = cws_get_option( "wave-style" ) == 1 ? true : false;

			$copyrights = cws_get_option( "footer_copyrights_text" );
			$social_links = "";
			$social_links_location = cws_get_option( 'social_links_location' );
			
			$show_wpml_footer = ( cws_is_wpml_active() && cws_show_flags_in_footer() ) ? true : false;
			if ( in_array( $social_links_location, array( 'bottom', 'top_bottom' ) ) ){
				$social_links = render_cws_social_links();	
			}			
			ob_start();
			if ( !empty( $copyrights ) ){
				echo "<div class='copyrights'>$copyrights</div>";
			}
			if ( (!empty( $social_links ) || !empty( $show_wpml_footer )) && empty( $copyrights )){
				echo "<div class='copyrights'></div>";
			}
			if ( !empty( $social_links ) || !empty( $show_wpml_footer ) ){
				echo "<div class='copyrights_panel'>";
					echo "<div class='copyrights_panel_wrapper'>";
						if($show_wpml_footer) : ?>
							<div class="lang_bar">
								<?php do_action('icl_language_selector'); ?>
				 		 	</div>
						<?php 	endif; 
						echo !empty( $social_links ) ? $social_links : "";
					echo "</div>";
				echo "</div>";
			}
			$copyrights_content = ob_get_clean();

			if ( (!empty( $footer_sidebar ) && is_active_sidebar( $footer_sidebar )) || !empty( $copyrights_content )){
				$footer_pattern_url = !empty($footer_pattern['url']) ? 'style="background-image: url('.$footer_pattern['url'].');"' :'';
				echo "<div class='footer_wrapper_copyright' $footer_pattern_url>";	
				if ( !empty( $footer_sidebar ) && is_active_sidebar( $footer_sidebar ) ){
					echo $use_wave ? "<div class='half_sin_wrapper'><canvas class='half_sin' data-bg-color='".cws_Hex2RGBwithdark(cws_get_option('theme-custom-color'),1.67)."' data-line-color='".cws_Hex2RGBwithdark(cws_get_option('theme-custom-color'),1.67)."'></canvas></div>" : '';
				}
			}
			if ( !empty( $footer_sidebar ) && is_active_sidebar( $footer_sidebar ) ){
				echo "<footer class='page_footer'>";
					echo "<div class='container'>";
						echo "<div class='footer_container'>";
							$GLOBALS['footer_is_rendered'] = true;
							dynamic_sidebar( $footer_sidebar );
							unset( $GLOBALS['footer_is_rendered'] );
						echo "</div>";
					echo "</div>";
				echo "</footer>";
			}
			if ( !empty( $copyrights_content ) ){
				echo "<div class='copyrights_area'>";
					echo $use_wave ? "<div class='half_sin_wrapper'><canvas class='footer_half_sin' data-bg-color='".cws_Hex2RGBwithdark(cws_get_option('theme-custom-color'),2.81)."' data-line-color='".cws_Hex2RGBwithdark(cws_get_option('theme-custom-color'),2.81)."'></canvas></div>" : '';
					echo "<div class='container'>";
						echo "<div class='copyrights_container'>";
							echo $copyrights_content;
						echo "</div>";
					echo "</div>";
				echo "</div>";
			}
			if ( (!empty( $footer_sidebar ) && is_active_sidebar( $footer_sidebar )) || !empty( $copyrights_content )){
				echo "</div>";
			}

		?>

	</div><!-- #page -->

	<div class='scroll_top animated'></div>

	<?php
	echo "</div>";
	?>

	<?php wp_footer(); ?>
		<!-- tuner -->
		<div id="tuner" class="tuner">
			<label class='main_panel_header'>Style Switcher</label>
			<div class="colors">
				<div id="color-1" class="color-picker" data-color="#3eb2cf" style="background-color:#3eb2cf;">Main</div>
				<div id="color-2" class="color-picker" data-color="#f8f2dc" style="background-color:#f8f2dc;">Secondary</div>
				<div id="color-3" class="color-picker" data-color="#f9e8b2"  style="background-color:#f9e8b2;">Outline</div>
				<div id="color-4" class="color-picker" data-color="#fec20b"  style="background-color:#fec20b;">Menu</div>
				<div id="color-5" class="color-picker" data-color="#fd8e00"  style="background-color:#fd8e00;">Menu Hover</div>
			</div>
			<div class='separate_line'></div>
			<div class="boxed-switcher-wrapp">
				<div class="title-boxed">Boxed</div>
				<div class="boxed-switcher">
					<div class="on">On</div>
					<div class="off active">Off</div>
				</div>
			</div>
			<div class='separate_line'></div>
			
			<div class="menu-switcher-wrapp">
				<div class="title-menu">theme header</div>
				<div class="menu-switcher">
					<div class="default active"><img src="wp-content/themes/kiddy-child/tuner/images/switcher_img/menu-01.png"></div>
					<div class="logo-out-menu"><img src="wp-content/themes/kiddy-child/tuner/images/switcher_img/menu-02.png"></div>
					<div class="without-bees"><img src="wp-content/themes/kiddy-child/tuner/images/switcher_img/menu-03.png"></div>
				</div>
			</div>
			<div class='separate_line'></div>

			<div class="style-switcher-wrapper">
				<div class="title-style-switcher">theme style</div>
				<div class="style-switcher">
					<div class="wave-style active"><img src="wp-content/themes/kiddy-child/tuner/images/switcher_img/theme-style-01.png"></div>
					<div class="flat-style"><img src="wp-content/themes/kiddy-child/tuner/images/switcher_img/theme-style-02.png"></div>
				</div>
			</div>
			<div class='separate_line'></div>

			<div class="patterns-wrapper">
				<div class="title-patterns">patterns</div>
				<ul class="patterns">
					<li data-pattern="1" class="color-blue active"></li>
					<li data-pattern="2" class="color-blue"></li>
					<li data-pattern="3" class="color-blue"></li>
					<li data-pattern="4" class="color-blue"></li>
					<li data-pattern="5" class="color-blue"></li>
					<li data-pattern="6" class="color-blue"></li>
					<li data-pattern="7" class="color-blue"></li>
					<li data-pattern="8" class="color-blue"></li>
					<li data-pattern="9" class="color-blue"></li>
				</ul>
			</div>
			
			<i id="tuner-switcher" class="fa fa-cogs"></i>
		</div>
		<div id="tuner-style-1" class="tuner-style" style="display: none;">
/* MAIN COLOR */
.item .date:before,
.item .date .month,
.cws_button:hover,
input[type='submit']:hover,
.page_footer .cws_button,
.page_footer .button,
.page_footer input[type='submit'],
.copyrights_area .cws_button,
.copyrights_area input[type='submit'],
blockquote,
table thead th,
.pagination .page_links>.page-numbers.current:before,
.pagination .page_links>span:not(.dots):before,
.gallery-icon a:before,
.pricing_table_column .top_section,
.pricing_table_column.active_table_column .btn_section>a,
.pricing_table_column.active_table_column .separate,
.pricing_table_column:hover .separate,
.tabs .tab,
.accordion_title .accordion_icon,
.accordion_section.active .accordion_title,
.cws_tweet .tweet-icon:before,
.cws_progress_bar .progress,
.cws_fa_wrapper .cws_fa:not(.alt),
a:hover>.cws_fa_wrapper .cws_fa.alt,
.cws_button.alt>span,
.testimonial,
.cws-widget .search-form:before,
.owl-pagination .owl-page,
#recentcomments>.recentcomments:before,
.comments-area .comment_list .reply .comment-reply-link:hover,
.comments-area .comment-respond .comment-form .submit,
.cws_ourteam .cws_ourteam_items .social_links a,
.news .media_part.only_link,
.footer_wrapper_copyright,
.mini-cart,
.mini-cart .button,
input[type="radio"]:checked:before,
.pic .links a,
.site_top_panel .cws_social_links:after,
.copyrights_area .cws_social_links .cws_social_link,
.copyrights_area .lang_bar ul ul,
.cws_callout,
.site_top_panel .site_top_panel_toggle:before,
.site_top_panel .site_top_panel_toggle:after,
.footer_container .woocommerce-product-search:before,
.scroll_top{
	background-color: #<span>cws_theme_color#</span>;
}
.tp-leftarrow.round,
.tp-rightarrow.round,
.rev_slider_wrapper{
	background-color: #<span>cws_theme_color#</span> !important;
}
.news .media_part,
.cws_ourteam_items .media_part,
.post_info,
.news .media_part,
.cws_img_frame, 
img[class*="wp-image-"],
.gallery-icon,
.pricing_table_column,
.pricing_table_column .top_section:after,
.accordion_title,
.accordion_content,
.carousel_header .carousel_nav i,
.cws_portfolio_items .item .media_part,
.carousel_nav_panel .prev,
.carousel_nav_panel .next,
.cws_fa_wrapper:hover>a>i + .ring,
a:hover>.cws_fa_wrapper>i:not(.alt) + .ring,
.cws_button.alt:hover,
.cws_oembed_wrapper,
.bordered,
.cws-widget .portfolio_item_thumb .pic,
.select2-container .select2-choice .select2-arrow,
.post_item .post_thumb_wrapp,
#recentcomments>.recentcomments,
.flxmap-container,
.wp-playlist .mejs-container,
.lang_bar>div:not(.lang_sel_list_vertical):not(.lang_sel_list_horizontal)>ul:before{
	border-color: #<span>cws_theme_color#</span>;
}
.select2-choice{
	border-color: #<span>cws_theme_color#</span> !important;
}
.select2-drop, 
.select2-drop-active{
	border-color: #<span>cws_theme_color#</span> !important;
}
.post_info,
.info i,
.post_info>*>i,
.post_info .comments_link a,
ul li:before,
.carousel_header .carousel_nav i,
.carousel_nav_panel .prev,
.carousel_nav_panel .next,
.cws_progress_bar .pb_title .indicator,
.select2-container .select2-choice .select2-arrow,
.comments-area .comment_list .comment-meta .author-name,
.cws_milestone,
.copyrights_container,
.mini-cart .total>*,
input[type=checkbox]:checked:before,
.banner_404,
.msg_404 span,
.cws_tweet:before,
.cws_fa_wrapper .cws_fa.alt,
.site_top_panel #top_panel_links .share-toggle-button,
.site_top_panel .cws_social_links .cws_social_link,
.site_top_panel #top_panel_links .search_icon,
.site_top_panel .mini-cart{
	color: #<span>cws_theme_color#</span>;
}
		</div>
		<div id="tuner-style-2" class="tuner-style" style="display: none;">
			/* SECONDARY COLOR */
canvas.breadcrumbs{
	color: #<span>cws_theme_secondary_color#</span>;
}
.page_title.flat{
	background: #<span>cws_theme_secondary_color#</span>;
}
		</div>
		<div id="tuner-style-3" class="tuner-style" style="display: none;">
			/* OUTLINE COLOR */
.pagination .page_links>*,
.pagination .page_links>.page-numbers.current:after,
.pagination .page_links>span:not(.dots):after,
.cws-widget #wp-calendar td,
.benefits_area{
	background: #<span>cws_theme_outline_color#</span>;
}
canvas.separator{
	color: #<span>cws_theme_outline_color#</span>;
}
.page_title.flat{
	border-color: #<span>cws_theme_outline_color#</span>;
}
		</div>	
		<div id="tuner-style-4" class="tuner-style" style="display: none;">
			/* MENU COLOR */
.header_logo_part.with_border .logo,
.main-nav-container .menu-item a,
.main-nav-container .menu-item:hover>.sub-menu>.menu-item,
.mobile_nav .menu-item,
.item .date .springs:before,
.item .date .springs:after,
.cws_button,
input[type='submit'],
.page_footer .cws_button:hover,
.page_footer .button:hover,
.page_footer input[type='submit']:hover,
.copyrights_area .cws_button:hover,
.copyrights_area input[type='submit']:hover,
.cws_callout .button_section>a:hover,
.pricing_table_column:hover .top_section,
.pricing_table_column.active_table_column .top_section,
.pricing_table_column.active_table_column .btn_section>a:hover,
.pricing_table_column .separate,
.tabs .tab.active,
.accordion_section.active .accordion_icon,
.separate,
.cws_fa_wrapper:hover>a .cws_fa,
a:hover>.cws_fa_wrapper .cws_fa:not(.alt),
.cws_button.alt:hover>span,
.dropcap,
.cws-widget .widget-title:after,
.cws-widget #wp-calendar tbody td#today,
.owl-pagination .owl-page.active,
.comments-area .comment_list .reply .comment-reply-link,
.comments-area .comment-respond .comment-form .submit:hover,
.cws_ourteam .cws_ourteam_items .social_links a:hover,
.mini-cart .button:hover,
.mini-cart:hover,
.site_top_panel .cws_social_links.expanded:after,
.pic .links .link a,
.pic .links .link-item-bounce,
.pic .links .link-toggle-button,
.pic .links_popup .link a,
.pic .links_popup .link span,
.pic .links_popup .link-item-bounce,
.pic .links_popup .link-toggle-button,
.cws_img_frame:after,
.gallery-icon a:after,
.mobile_menu_header .mobile_menu_switcher,
.post_item .post_thumb_wrapp .links span,
.ce_toggle.alt .accordion_section .accordion_content, 
.ce_accordion.alt .accordion_section .accordion_content,
body.wave-style hr,
.item .date.def_style .month:before,
.scroll_top:hover{
	background-color: #<span>cws_menu_color#</span>;
}
.cws-widget .search-form{
	background-color: #<span>cws_menu_color#</span> !important;
}
.bees:after,
.item .date,
.pricing_table_column:hover,
.pricing_table_column.active_table_column,
.pricing_table_column:hover .top_section:after,
.pricing_table_column.active_table_column .top_section:after,
.cws_tweet .tweet-icon,
.cws_fa_wrapper .ring,
.cws_button.alt,
.comments-area .comment_list .avatar,
.comments-area .comment-respond,
.tp-leftarrow.round,
.tp-rightarrow.round,
.site_top_panel form.search-form .search-field,
.testimonial .author img{
	border-color: #<span>cws_menu_color#</span>;
}
.post_info a,
.post_info>*,
.info,
.post_info .v_sep,
.bread-crumbs .delimiter,
blockquote:before,
blockquote:after,
.carousel_header .carousel_nav i:hover,
.carousel_nav_panel .prev:hover,
.carousel_nav_panel .next:hover,
.cws_progress_bar .pb_title,
.testimonial .quote .quote_link,
.testimonial .quote + .author>.dott>span:first-child,
.testimonial .quote + .author>.dott>span:last-child,
.testimonial:before,
.testimonial:after,
#recentcomments .comment-author-link a,
.comments-area .comment_list .comment-meta .comment_date,
.comments-area .required,
.cws_ourteam:not(.single) .cws_ourteam_items .positions a,
.footer_container .cws-widget .post_item .post_date,
.footer_container .post-date,
.woo_mini_cart .total>.amount,
.banner_404 span,
.cws_tweet .tweet_date,
.p_cut,
.news .more-link,
.site_top_panel.show-search #top_panel_links .search_icon,
.site_top_panel .mini-cart:hover,
.footer_container ul.product_list_widget li a,
.footer_container ul.product_list_widget ins,
.footer_container ul.product_list_widget span.amount{
	color: #<span>cws_menu_color#</span>;
}
		</div>	
		<div id="tuner-style-5" class="tuner-style" style="display: none;">
			/* MENU COLOR HOVER */
.main-nav-container .menu-item:hover>a,
.header_nav_part.mobile_nav .main-nav-container .menu-item.current-menu-ancestor,
.header_nav_part.mobile_nav .main-nav-container .menu-item.current-menu-item,
.main-nav-container .menu-item.current-menu-ancestor>a,
.main-nav-container .menu-item.current-menu-item>a,
.main-nav-container .sub-menu .menu-item:hover>a,
.main-nav-container .sub-menu .menu-item.current-menu-ancestor>a,
.main-nav-container .sub-menu .menu-item.current-menu-item>a,
.cws_callout .button_section>a:hover{
	background-color: #<span>cws_menu_color_hover#</span>; 
}
.main-nav-container .menu-item:hover>a>.bees:after,
.main-nav-container .menu-item.current-menu-item>a>.bees:after,
.main-nav-container .menu-item.current-menu-ancestor>a>.bees:after,
.header_logo_part.with_border .logo{
	border-color: #<span>cws_menu_color_hover#</span>; 
}
		</div>			
		<!--/ tuner -->	
</body>
</html>