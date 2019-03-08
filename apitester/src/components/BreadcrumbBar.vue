<template>
<div class="container-fluid d-flex align-items-center breadcrumb-bar">
	<b-breadcrumb class="offset-md-2 breadcrumb" :items="$application.breadcrumb"/>
</div>
</template>

<script>
export default {
	name: 'BreadcrumbBar',

	data () {
		return {
			items: [
				 {
					 text: 'Manufacturers',
					 href: '/manufacturers'
				 }, {
				 	text: 'Workshop'
				 }, {
				 	text: 'Engineers Time Entries'
				 }
			]
		}
	},

	computed: {

	}
}
</script>

<style lang="less" scoped>
@import '../assets/colours';

* {
	background-color: @grey;
}

.container-fluid {
	height: 40px;
}

.breadcrumb-bar {
	box-shadow: inset 0 -1px 0 rgba(0, 0, 0, .1);
}

.breadcrumb {
	padding: 0px;
	margin-bottom: 0px;
}
</style>
