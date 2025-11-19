export const clienteSchema = [
  {
    $formkit: "text",
    name: "nombre",
    label: "Escriba el nombre",
    validation: "required|length:3",
    inputClass: "input input-bordered w-full",
    outerClass: "mb-4 flex flex-col gap-1",
    validationMessages: {
      required: "El nombre es obligatorio",
      length: "Debe tener al menos 3 caracteres",
    },
  },
  {
    $formkit: "email",
    label: "Escriba el Email",
    inputClass: "input input-bordered w-full",
    outerClass: "mb-4 flex flex-col gap-1",
    validation: "required|email|ends_with:.com",
     validationMessages: {
      required: "El email es obligatorio",
      email: "Debe ser un email válido",
      ends_with: "El email debe terminar en .com",
    },
  },
  {
    $formkit: "tel",
    label: "Número de teléfono",
    placeholder: "xxx-xxx-xxxx",
    inputClass: "input input-bordered w-full",
    outerClass: "mb-4 flex flex-col gap-1",
    validation: "required|matches:/^[0-9]{3}-[0-9]{3}-[0-9]{4}$/",
    validationVisibility: "dirty",
    validationMessages: {
      required: "El número de teléfono es obligatorio.",
      matches: "Debe tener el formato xxx-xxx-xxxx.",
    },
  },
];
