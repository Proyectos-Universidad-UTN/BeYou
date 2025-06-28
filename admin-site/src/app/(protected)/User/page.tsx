"use client";

import { Page } from "@/components/Shared/Page";
import { PageHeader } from "@/components/Shared/PageHeader";
import { UseGetUser } from "@/hooks/api-beyou/user/UseGetUser";
import { UserTable } from "./UserTable";
import { CircularLoadingProgress } from "@/components/LoadingProgress/CircularLoadingProcess";
import { UserTableItem } from "./UserTable";

export default function UserPage() {
  const { data, isLoading, error } = UseGetUser();

  const users: UserTableItem[] = (data ?? [])
    .filter((user) => user.id !== undefined)
    .map((user) => ({
      id: user.id!,
      firstName: user.firstName,
      lastName: user.lastName,
      email: user.email,
      telephone: user.telephone,
      roleId: user.roleId,
      genderId: user.genderId,
    }));

  return (
    <Page header={<PageHeader title="Usuarios" />}>
      {isLoading ? (
        <CircularLoadingProgress />
      ) : error ? (
        <p>Error al cargar usuarios</p>
      ) : users.length === 0 ? (
        <p>No se encontró información</p>
      ) : (
        <UserTable users={users} />
      )}
    </Page>
  );
}
