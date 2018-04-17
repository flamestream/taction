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
import keyDefinitions from '@/definitions/keys';
import StringType from '@/types/StringType';

const labelMap = {
	'CONTROL': 'CTRL',
	'LCONTROL': 'L-CTRL',
	'RCONTROL': 'R-CTRL',
	'MENU': 'ALT',
	'LMENU': 'L-ALT',
	'RMENU': 'R-ALT',
	'SHIFT': 'SHIFT',
	'LSHIFT': 'L-SHIFT',
	'RSHIFT': 'R-SHIFT'
};

const ctrlStatusMap = {
	1: 'CONTROL',
	2: 'RCONTROL',
	3: 'LCONTROL'
};

const altStatusMap = {
	1: 'MENU',
	2: 'RMENU',
	3: 'LMENU'
};

const shiftStatusMap = {
	1: 'SHIFT',
	2: 'RSHIFT',
	3: 'LSHIFT'
};

const modifiers = Object.keys(labelMap);

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

			if (this.keys.includes('LCONTROL'))
				return 3;
			else if (this.keys.includes('RCONTROL'))
				return 2;
			else if (this.keys.includes('CONTROL'))
				return 1;

			return 0;
		},
		altStatus() {

			if (this.keys.includes('LMENU'))
				return 3;
			else if (this.keys.includes('RMENU'))
				return 2;
			else if (this.keys.includes('MENU'))
				return 1;

			return 0;
		},
		shiftStatus() {

			if (this.keys.includes('LSHIFT'))
				return 3;
			if (this.keys.includes('RSHIFT'))
				return 2;
			else if (this.keys.includes('SHIFT'))
				return 1;

			return 0;
		},
		ctrlKeyName() {
			return ctrlStatusMap[this.ctrlStatus];
		},
		altKeyName() {
			return altStatusMap[this.altStatus];
		},
		shiftKeyName() {
			return shiftStatusMap[this.shiftStatus];
		},
		ctrlLabel() {
			return labelMap[ctrlStatusMap[this.ctrlStatus || 1]];
		},
		altLabel() {
			return labelMap[altStatusMap[this.altStatus || 1]];
		},
		shiftLabel() {
			return labelMap[shiftStatusMap[this.shiftStatus || 1]];
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
			newState.ctrlStatus && (value += ctrlStatusMap[newState.ctrlStatus] + ' ');
			newState.altStatus && (value += altStatusMap[newState.altStatus] + ' ');
			newState.shiftStatus && (value += shiftStatusMap[newState.shiftStatus] + ' ');
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
			let ctrlStatus = (this.ctrlStatus + 1) % 4;
			this.updateValue({ ctrlStatus });
		},
		onAltClick() {
			let altStatus = (this.altStatus + 1) % 4;
			this.updateValue({ altStatus });
		},
		onShiftClick() {
			let shiftStatus = (this.shiftStatus + 1) % 4;
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
	font-size: 16px;
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

.keyname .button:active {
	top: 0;
	left: 0;
}

.keyname .button i {
	font-size: 16px;
}

.button:active i {
	position: relative;
	top: 1px;
	left: 1px;
}

</style>
