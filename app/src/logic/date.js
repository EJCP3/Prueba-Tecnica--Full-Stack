import { ref } from "vue";

export function date() {
  const fecha = ref(new Date());

  const dia = ref(fecha.value.getDate()); 
  const mes = ref(fecha.value.getMonth() + 1);
  const anio = ref(fecha.value.getFullYear()); 
  const hora = ref(fecha.value.getHours()); 
  const minutos = ref(fecha.value.getMinutes());
  const segundo = ref(fecha.value.getSeconds()) 

  // Para mostrar la fecha en un  formato tipo "dd/mm/yyyy hh:mm"
  const fechaFormateada = ref(
    `${dia.value.toString().padStart(2, "0")}/` +
      `${mes.value.toString().padStart(2, "0")}/` +
      `${anio.value} ${hora.value.toString().padStart(2, "0")}:` +
      `${minutos.value.toString().padStart(2, "0")}` + 
      ` ${segundo.value.toString().padStart(2, "0")}`
  );

  return fechaFormateada;
}
