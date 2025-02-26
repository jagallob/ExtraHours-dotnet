import axios from "axios";

export const UserService = {
  login: async (email, password) => {
    try {
      const response = await fetch("http://localhost:5224/auth/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ email, password }),
      });
      if (!response.ok) {
        throw new Error(`Error ${response.status}: ${response.statusText}`);
      }

      const data = await response.json();

      localStorage.setItem("token", data.token);
      localStorage.setItem("id", data.id);
      localStorage.setItem("role", data.role);

      return data;
    } catch (error) {
      console.error("Login error:", error);
      throw error;
    }
  },

  changePassword: async (currentPassword, newPassword) => {
    try {
      const token = localStorage.getItem("token");
      const id = localStorage.getItem("id");

      if (!id) {
        throw new Error("ID del usuario no encontrado");
      }

      const response = await fetch(
        `https://localhost:5224/auth/change-password?id=${id}`,
        {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
          body: JSON.stringify({ currentPassword, newPassword }),
        }
      );

      if (!response.ok) {
        throw new Error(`Error ${response.status}: ${response.statusText}`);
      }

      return await response.text();
    } catch (error) {
      console.error("Change password error:", error);
      throw error;
    }
  },
};

export const logout = async () => {
  try {
    const token = localStorage.getItem("token");
    const response = await axios.post(
      "https://localhost:5224/api/logout",
      {},
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );
    localStorage.removeItem("token"); // Eliminar token del almacenamiento local
    return response.data;
  } catch (error) {
    console.error("Logout error:", error);
    throw error;
  }
};
