<template>
	<tr>
		<th>{{ label }}</th>
		<td v-if="type === 'number'"><input type="number" :value="value"/></td>
		<td v-else-if="type === 'number-positive'"><input type="number" :value="value" min="0"/></td>
		<td v-else-if="type === 'number-100'"><input type="range" min="0" max="1" step="0.01" :value="value || 1"/></td>
		<td v-else-if="type === 'boolean'"><input type="checkbox" :value="value === 'true'"/></td>
		<td v-else-if="type === 'rectangle'"><PropertyRowTypeRectangle :value="value"/></td>
		<td v-else-if="type === 'border'"><PropertyRowTypeBorder :value="value"/></td>
		<td v-else-if="type === 'color'"><PropertyRowTypeColor :value="value"/></td>
		<td v-else-if="type === 'orientation'">
			<select :value="value">
				<option>vertical</option>
				<option>horizontal</option>
			</select>
		</td>
		<td v-else><input type="text" :value="value"/></td>
	</tr>
</template>

<script>
import PropertyRowTypeRectangle from './PropertyRowTypeRectangle'
import PropertyRowTypeColor from './PropertyRowTypeColor'
import PropertyRowTypeBorder from './PropertyRowTypeBorder'
export default {
	name: 'PropertyRow',
	components: {
		PropertyRowTypeRectangle,
		PropertyRowTypeColor,
		PropertyRowTypeBorder
	},
	props: [ 'label', 'value', 'type' ]
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

th {
	text-align: left;
}

input[type=number] {
	width: 4em;
}

</style>
