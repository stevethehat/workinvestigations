<template>
	<nav class="col-md-2 d-none d-md-block sidebar" ref="sideBar">
		<ul class="nav flex-column">
			<li v-for="item in $application.menu" :key="item.href" class="nav-item">
				<router-link class="nav-link" :to="item.href">
                    <span v-if="null === item.icon">
                        {{item.label}}
                    </span>
                    <span v-else>
                        <i :class="'far fa-fw ' + item.icon"></i>{{item.label}}
                    </span>
				</router-link>
				<ul>
					<li v-for="subitem in item.items" :key="subitem.href">
						<router-link class="nav-sub-link" :to="subitem.href">
                            <span v-if="null === subitem.icon">
                                {{subitem.label}}
                            </span>
                            <span v-else>
                                <i :class="'far fa-fw ' + subitem.icon"></i>{{subitem.label}}
                            </span>
						</router-link>
					</li>
				</ul>
			</li>
		</ul>
	</nav>
</template>

<script>
export default {
	name: 'SideNavBar'
};
</script>

<style lang="less" scoped>
@import '../assets/colours';

.sidebar {
	background: @grey;
	position: fixed;
	top: 110px;
	bottom: 70px;
	left: 0;
	z-index: 100; /* Behind the navbar */
	padding: 0.5rem;
	box-shadow: inset -1px 0 0 rgba(0, 0, 0, .1);
}

.sidebar .nav-link, .nav-sub-link{
	font-weight: 500;
	color: #333;
}

.sidebar .nav-link .feather {
	margin-right: 4px;
	color: #999;
}

.sidebar .nav-link.router-link-exact-active {
  	color: #007bff;
}

.sidebar .nav-link:hover .feather,
.sidebar .nav-link.active .feather {
  	color: inherit;
}

.sidebar-heading {
	font-size: .75rem;
	text-transform: uppercase;
}

.nav-sub-link{
	margin-left: 30px;
}
</style>
