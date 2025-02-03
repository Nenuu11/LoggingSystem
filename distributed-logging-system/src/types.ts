import { HttpContext, HttpHeaders, HttpParams } from "@angular/common/http";

export interface ApiResponse{
  statusCode: number,
  data: object
}

export interface Log {
    id?: number,
    service: string;
    level: string;
    message: string;
    timestamp: Date;
  }

  export interface Logs {
    items: Log[];
    total: number;
    page: number;
    perPage: number;
    totalPages: number;
  }
  

  export interface Options {
    headers?:
      | HttpHeaders
      | {
          [header: string]: string | string[];
        };
    observe?: 'body';
    context?: HttpContext;
    params?:
      | HttpParams
      | {
          [param: string]:
            | string
            | number
            | boolean
            | ReadonlyArray<string | number | boolean>;
        };
    reportProgress?: boolean;
    responseType?: 'json';
    withCredentials?: boolean;
    transferCache?:
      | {
          includeHeaders?: string[];
        }
      | boolean;
  }
  
  export interface PaginationParams {
    [param: string]:
          | string
          | number
          | boolean
          | ReadonlyArray<string | number | boolean>;
  page: number;
  perPage: number;
}
