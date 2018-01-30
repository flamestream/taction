<template>
	<div class="hello">
		Preview
		{{ state.layout }}
	</div>
</template>

<script>
export default {
	name: 'ViewPreview',
	props: ['state'],
	data() {
		return {
			msg: 'Welcome to Your Vue.js App'
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
div {
	background-color: #FF000055;
}
</style>
