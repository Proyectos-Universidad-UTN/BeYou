import Image from "next/image";
import Link from "next/link";

export default function NotFound() {
  return (
    <div className="flex flex-col items-center justify-center min-h-screen bg-pink-100">
      <div className="relative w-[85vw] h-[85vh]">
        <Image
          src="/assets/not-found.webp"
          alt="Página no encontrada"
          fill
          className="object-contain rounded-xl shadow-lg"
          priority
        />
      </div>

      <Link
        href="/Dashboard"
        className="mt-6 text-pink-600 underline text-lg hover:text-pink-800 transition"
      >
        Volver al inicio
      </Link>
    </div>
  );
}
