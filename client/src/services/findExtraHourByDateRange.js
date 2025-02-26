export const findExtraHourByDateRange = async (startDate, endDate) => {
  try {
    const token = localStorage.getItem("token");
    if (!token) {
      console.error("Token no encontrado en localStorage");
      return;
    }

    const response = await fetch(
      `https://localhost:5224/api/extra-hour/date-range-with-employee?startDate=${startDate}&endDate=${endDate}`,
      {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      }
    );

    if (!response.ok) {
      throw new Error("Error al obtener la información de rango de fechas");
    }

    const data = await response.json();

    return data.map((record) => ({
      ...record.extraHour,
      ...record.employee,
    }));
  } catch (error) {
    console.error("Error al buscar fecha:", error);

    throw error;
  }
};
