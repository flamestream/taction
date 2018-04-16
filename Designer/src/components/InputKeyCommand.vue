<template>
	<div class="root">
		<div class="modifiers">
			<label :class="ctrlClassNames" @click="onCtrlClick">{{ ctrlLabel }}</label>
			<label :class="altClassNames" @click="onAltClick">{{ altLabel }}</label>
			<label :class="shiftClassNames" @click="onShiftClick">{{ shiftLabel }}</label>
		</div>
		<div class="keyname">
			<input type="text" @keydown.prevent="onKeyDown" :value="keyName" spellcheck="false" autocomplete="false" />
			<label class="button" @click="onClearClick"><i class="material-icons">clear</i></label>
		</div>
	</div>
</template>

<script>
import InputText from './InputText'
import keyDefinitions from '@/definitions/keys';
import StringType from '@/types/StringType';

let modifiers = [
	'CONTROL', 'RCONTROL',
	'MENU', 'RMENU',
	'SHIFT', 'RSHIFT',
];

export default {
	name: 'InputKeyCommand',
	components: {
	},
	props: {
		obj: { type: StringType }
	},
	computed: {
		value() {
			let { obj } = this;
			return obj.value;
		},
		keys() {
			let str = this.value;
			if (typeof str !== 'string')
				str = '';

			return str.split(' ').filter(c => c);
		},
		keyName() {

			// Conflict handling: first of non-known modifier
			let out;
			for (let key of this.keys) {
				if (modifiers.includes(key)) continue;
				out = key;
				break;
			}

			return out;
		},
		ctrlStatus() {

			if (this.keys.includes('RCONTROL'))
				return 2;
			else if (this.keys.includes('CONTROL'))
				return 1;

			return 0;
		},
		altStatus() {

			if (this.keys.includes('RMENU'))
				return 2;
			else if (this.keys.includes('MENU'))
				return 1;

			return 0;
		},
		shiftStatus() {

			if (this.keys.includes('RSHIFT'))
				return 2;
			else if (this.keys.includes('SHIFT'))
				return 1;

			return 0;
		},
		ctrlLabel() {
			return this.ctrlStatus === 2 ? 'R-CTRL' : 'CTRL';
		},
		altLabel() {
			return this.altStatus === 2 ? 'R-ALT' : 'ALT';
		},
		shiftLabel() {
			return this.shiftStatus === 2 ? 'R-SHIFT' : 'SHIFT';
		},
		ctrlClassNames() {
			return {
				button: true,
				inactive: !this.ctrlStatus
			}
		},
		altClassNames() {
			return {
				button: true,
				inactive: !this.altStatus
			}
		},
		shiftClassNames() {
			return {
				button: true,
				inactive: !this.shiftStatus
			}
		}
	},
	methods: {
		updateValue(updated) {

			let newState = Object.assign({
				ctrlStatus: this.ctrlStatus,
				altStatus: this.altStatus,
				shiftStatus: this.shiftStatus,
				keyName: this.keyName
			}, updated);

			let value = '';
			newState.ctrlStatus && (value += (newState.ctrlStatus === 2) ? 'RCONTROL ' : 'CONTROL ');
			newState.altStatus && (value += (newState.altStatus === 2) ? 'RMENU ' : 'MENU ');
			newState.shiftStatus && (value += (newState.shiftStatus === 2) ? 'RSHIFT ' : 'SHIFT ');
			value += newState.keyName || '';

			this.$store.commit({
				type: 'layout/setValue',
				obj: this.obj,
				value
			});

		},
		onKeyDown(ev) {

			// Modifier check
			let { keyCode } = ev;
			let keyName = keyDefinitions.names[keyCode];
			if (modifiers.includes(keyName))
				return;

			ev.target.blur();
			this.updateValue({ keyName })
		},
		onCtrlClick() {
			let ctrlStatus = (this.ctrlStatus + 1) % 3;
			this.updateValue({ ctrlStatus });
		},
		onAltClick() {
			let altStatus = (this.altStatus + 1) % 3;
			this.updateValue({ altStatus });
		},
		onShiftClick() {
			let shiftStatus = (this.shiftStatus + 1) % 3;
			this.updateValue({ shiftStatus });
		},
		onClearClick() {
			this.updateValue({ keyName: null });
		}
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

.root {
	display: flex;
	flex-direction: column;
}

.modifiers {
	display: flex;
	justify-content: center;
	margin-bottom: 8px;
}

.modifiers label {

	width: 60px;
	margin: 0px 4px;
}

.modifiers .button.inactive {

	opacity: 0.3;
}

.keyname {
	display: flex;
}

.keyname input {
	flex: 1 1 auto;
	border-top-right-radius: 0;
	border-bottom-right-radius: 0;
}

.keyname .button {
	font-size: 0;
	border-top-left-radius: 0;
	border-bottom-left-radius: 0;
}

.keyname .button:hover {
	color: #E00;
	background-color: #DDD;
}

.keyname .button:active {
	top: 0;
	left: 0;
	background-color: #E00;
	color: #FFF;
}


</style>
