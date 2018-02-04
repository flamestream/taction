<template>
	<select v-model="value">
		<option v-for="(file, name) in files" :key="name">{{ name }}</option>
	</select>
</template>

<script>
export default {
	name: 'InputLoadedFile',
	props: ['filter'],
	data() {
		return {
			value: undefined
		}
	},
	computed: {
		files() {
			return this.$root.$data;
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
