<template>
	<div>
		<table>
			<tr>
				<th>Thickness</th>
				<td><PropertyRowTypeRectangle :value="value && value.thickness" /></td>
			</tr>
			<tr>
				<th>Roundness</th>
				<td><PropertyRowTypeRectangle :value="value && value.radius" /></td>
			</tr>
			<tr>
				<th>Color</th>
				<td><PropertyRowTypeColor :value="value && value.color" /></td>
			</tr>
		</table>
	</div>
</template>

<script>
import PropertyRowTypeRectangle from './PropertyRowTypeRectangle'
import PropertyRowTypeColor from './PropertyRowTypeColor'
export default {
	name: 'PropertyRowTypeBorder',
	components: {
		PropertyRowTypeRectangle,
		PropertyRowTypeColor
	},
	props: ['value']
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
