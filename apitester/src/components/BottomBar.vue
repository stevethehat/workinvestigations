<template>
<footer class="footer fixed-bottom bg-dark">
	<div class="container-fluid d-flex justify-content-between align-items-end h-100">
		<div class="info">
			<span>v{{ goldVersion }}</span>
			<span>Company: {{ companyId }} {{ companyName }}</span>
			<span>Depot: {{ depotId }}  {{ depotName }}</span>
		</div>
		<img src="@/assets/ibcos_white_banner_h50px.png">
	</div>
</footer>
</template>

<script>
import { mapGetters } from 'vuex';
//import CompanyApi from '@/common/company';

export default {
	name: 'BottomBar',

	data(){
		return {
			goldVersion: null
		}
	},

	created() {
		this.fetchVersion();
	},

	methods: {
		fetchVersion(){
			const me = this;

			CompanyApi
				.getInfo()
				.then(
					success => {
						let info = success.data;

						me.goldVersion = info.GOLD_Version;
					}
				);			
		}
	},

	computed :{
		...mapGetters([
			'company',
			'depot'
		]),

		depotId() {
			let id = "All";

			if (null != this.depot)
			{
				id = this.depot.Id;
			}

			return id;
		},

		depotName() {
			let depotName = null;

			if (null != this.depot)
			{
				depotName = this.depot.Name;
			}

			return depotName;
		},

		companyId() {
			let id = null;

			if (null != this.company)
			{
				id = this.company.Id;
			}

			return id;
		},

		companyName() {
			let companyName = null;

			if (null != this.company)
			{
				companyName = this.company.Name;
			}

			return companyName;
		}		
	}
}
</script>

<style scoped>
footer {
	height: 70px;
	padding: 20px 0px;
}

span {
	color: #ddd;
	line-height: 14px;
	margin: 0 15px;
}

img {
	height: 30px;
}

.container-fluid {
	padding: 0px 20px;
}
</style>
