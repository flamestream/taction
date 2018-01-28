<template>
	<tr>
		<th>{{ label }}</th>
		<td><input type="text" v-bind:value="value"/></td>
	</tr>
</template>

<script>
export default {
	name: 'PropertyRowText',
	props: [ 'label', 'value' ]
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
table {
	background-color: #FF00FF55;
}
</style>
