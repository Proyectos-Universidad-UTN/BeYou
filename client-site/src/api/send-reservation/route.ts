import nodemailer from "nodemailer";

export async function POST(request: Request) {
  try {
    const body = await request.json();
    const { firstName, lastName, email, phone, address, date, hour } = body;

    // Datos fijos de la tienda
    const storeEmail = "nee2015primaria@gmail.com";
    const storePhone = "+506 60106969";

    // Configuración del transporte de correo
    const transporter = nodemailer.createTransport({
      service: "gmail",
      auth: {
        user: storeEmail,
        pass: process.env.EMAIL_PASSWORD, // desde .env.local
      },
    });

    // Contenido del correo
    const mailOptions = {
      from: storeEmail,
      to: storeEmail, // Puedes cambiar a otro destinatario
      subject: "Nueva Reservación",
      text: `
📅 Fecha: ${date}
⏰ Hora: ${hour}
👤 Cliente: ${firstName} ${lastName}
📧 Email cliente: ${email}
📞 Teléfono cliente: ${phone}
🏠 Dirección: ${address}

📞 Teléfono tienda: ${storePhone}
📧 Correo tienda: ${storeEmail}
      `,
    };

    // Enviar el correo
    await transporter.sendMail(mailOptions);

    return new Response(
      JSON.stringify({ message: "Correo enviado con éxito" }),
      { status: 200 }
    );
  } catch (error) {
    console.error("❌ Error enviando correo:", error);
    return new Response(
      JSON.stringify({ message: "Error enviando correo" }),
      { status: 500 }
    );
  }
}
