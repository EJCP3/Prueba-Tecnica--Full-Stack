
 export const productoSchema = [
  

    // Nombre
    {
      $formkit: 'text',
      name: 'nombre',
      label: 'Nombre',
      placeholder: 'Nombre',
      validation: 'required',
      validationMessages: {
        required: 'El nombre es obligatorio'
      },
      inputClass: 'input',
      labelClass: 'label text-base',
      messagesClass: 'text-red-500'
    },

    // Descripción
    {
      $formkit: 'text',
      name: 'descripcion',
      label: 'Descripcion',
      placeholder: 'Descripcion',
      validation: 'required',
      validationMessages: {
        required: 'La descripción es obligatoria'
      },
      inputClass: 'input',
      labelClass: 'label text-base',
      messagesClass: 'text-red-500'
    },

    // Precio
    {
      $formkit: 'text',
      name: 'precio',
      label: 'Precio',
      placeholder: 'Precio',
      validation: 'required|number',
      validationMessages: {
        required: 'El precio es obligatorio',
        number: 'El precio debe ser un número'
      },
      inputClass: 'input',
      labelClass: 'label text-base',
      messagesClass: 'text-red-500'
    },

    // Stock
    {
      $formkit: 'text',
      name: 'stock',
      label: 'Stock',
      placeholder: 'Stock',
      validation: 'required|number',
      validationMessages: {
        required: 'El stock es obligatorio',
        number: 'El stock debe ser un número'
      },
      inputClass: 'input',
      labelClass: 'label text-base',
      messagesClass: 'text-red-500'
    },

    // Botón
    {
      $formkit: 'submit',
      label: 'Guardar',
      inputClass: 'btn btn-primary btn-md mt-4'
    }
  ];

