<template>
  <div
    v-if="modelValue"
    class="fixed inset-0 flex items-center justify-center bg-black/50 p-4 z-50"
  >
    <div class="bg-base-200 rounded-xl shadow-xl p-6 w-full max-w-sm text-center">
      <h2 class="text-xl font-semibold mb-3 text-red-500">{{ title }}</h2>

      <p class="text-green-300 mb-6">
        {{ message }}
      </p>

      <button
        class="btn btn-neutral w-full"
        @click="$emit('update:modelValue', false)"
      >
        Cerrar
      </button>
    </div>
  </div>
</template>

<script setup>
defineProps({
  modelValue: { type: Boolean, required: true },
  title: { type: String, default: "Error" },
  message: {
    type: String,
    default: "Ha ocurrido un error. Revisa los campos.",
  },
});

defineEmits(["update:modelValue"]);
</script>
