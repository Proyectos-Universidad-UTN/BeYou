import { components } from "@/api/clients/beyou/api";

type SchemaTypes = keyof components['schemas'];
export type SchemaData = components['schemas'][SchemaTypes];

export type Roles = 'Administrator'

export type ErrorDetailsBeYou = components['schemas']['ErrorDetailsBeYou']

export type User = components['schemas']['ResponseUserDto']

export type UserProfile = components['schemas']['ResponseMeDto']

export type UserList = components['schemas']['ResponseUserDto'][]

export type Branch = components['schemas']['ResponseBranchDto']
export type BranchRequest = components['schemas']['RequestBranchDto']

export type BranchSchedule = components['schemas']['ResponseBranchScheduleDto']
export type BranchScheduleRequest = components['schemas']['RequestBranchScheduleDto']

export type BranchScheduleBlock = components['schemas']['ResponseBranchScheduleBlockDto']
export type BranchScheduleBlockRequest = components['schemas']['RequestBranchScheduleBlockDto']

export type Province = components['schemas']['ResponseProvinceDto']
export type Canton = components['schemas']['ResponseCantonDto']
export type District = components['schemas']['ResponseDistrictDto']

export type WeeklyDay = components['schemas']['WeekDayApplication']
export type Schedule = components['schemas']['ResponseScheduleDto']
export type ScheduleRequest = components['schemas']['RequestScheduleDto']

export type LoginUserRequest = components['schemas']['RequestUserLoginDto']
export type UserTokenRefreshRequest = components['schemas']['TokenModel']
export type Authentication = components['schemas']['AuthenticationResult']

export type Holiday = components['schemas']['ResponseHolidayDto']
export type HolidayRequest = components['schemas']['RequestHolidayDto']
export type MonthName = components['schemas']['MonthApplication']

export type Tax = components['schemas']['ResponseTaxDto']
export type TaxRequest = components['schemas']['RequestTaxDto']

export type UnitMeasure = components['schemas']['ResponseUnitMeasureDto']
export type UnitMeasureRequest = components['schemas']['RequestUnitMeasureDto']