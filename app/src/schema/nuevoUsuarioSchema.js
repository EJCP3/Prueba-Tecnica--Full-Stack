export const userSchema = [
  {
    $formkit: "text",
    name: "nombre",
    label: "Nombre",
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
    name: "email",
    label: "Correo electrónico",
    validation: "required|email|ends_with:.com",
    inputClass: "input input-bordered w-full",
    outerClass: "mb-4 flex flex-col gap-1",
    validationMessages: {
      required: "El email es obligatorio",
      email: "Debe ser un email válido",
      ends_with: "El email debe terminar en .com",
    },
  },

  {
    $formkit: "text",
    name: "username",
    label: "Nombre de usuario",
    validation: "required|length:3",
    inputClass: "input input-bordered w-full",
    outerClass: "mb-4 flex flex-col gap-1",
    validationMessages: {
      required: "El username es obligatorio",
      length: "Debe tener al menos 3 caracteres",
    },
  },



  {
    $formkit: "select",
    name: "rol",
    label: "Rol del usuario",
    options: [
      { label: "user", value: "user" },
      { label: "admin", value: "admin" },
    ],
    validation: "required",
    inputClass: "select select-bordered w-full",
    outerClass: "mb-4 flex flex-col gap-1",
    validationMessages: {
      required: "Debe seleccionar un rol",
    },
  },

  {
    $formkit: "password",
    name: "passwordHash",
    label: "Contraseña",
    validation: "required|length:6",
    inputClass: "input input-bordered w-full",
    outerClass: "mb-4 flex flex-col gap-1",
    validationMessages: {
      required: "La contraseña es obligatoria",
      length: "Debe tener al menos 6 caracteres",
    },
  },
  
];
