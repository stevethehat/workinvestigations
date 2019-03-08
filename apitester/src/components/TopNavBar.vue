<template>
<nav class="navbar navbar-dark bg-dark flex-md-nowrap align-items-center d-flex p-0" ref="topBar">
	<h1 class="offset-md-2 text-white">Price Updates</h1>
</nav>
</template>

<script>
export default {
	data() {
		return {
			pageTitle: 'hi this is the page title'
		}
	},
	name: 'TopNavBar',
    methods: {
		
	}
};
</script>

<style scoped>
nav {
	height: 70px;
}

h1 {
	margin-bottom: 0;
}

.navbar-brand {
	padding-top: .75rem;
	padding-bottom: .75rem;
	font-size: 1rem;
}

.navbar .form-control {
	padding: .75rem 1rem;
	border-width: 0;
	border-radius: 0;
}

.form-control-dark {
	color: #fff;
	background-color: rgba(255, 255, 255, .1);
	border-color: rgba(255, 255, 255, .1);
}

.form-control-dark:focus {
	border-color: transparent;
	box-shadow: 0 0 0 3px rgba(255, 255, 255, .25);
}

.logout-icon {
	vertical-align: middle;
}
</style>
