BEGIN
  DBMS_SCHEDULER.CREATE_JOB(
    job_name        => 'JOB_FINALIZAR_RESERVAS_VENCIDAS',
    job_type        => 'PLSQL_BLOCK',
    job_action      => q'[
      BEGIN
        UPDATE RESERVA
           SET ESTADO='FINALIZADA'
         WHERE ESTADO='RESERVADA' AND FIN_TS <= SYSTIMESTAMP;

        UPDATE BAHIA b
           SET b.ID_ESTADO = (SELECT ID_ESTADO FROM ESTADO_BAHIA WHERE CODIGO='DISPONIBLE')
         WHERE NOT EXISTS (
           SELECT 1 FROM RESERVA r
            WHERE r.ID_BAHIA = b.ID_BAHIA
              AND r.ESTADO IN ('RESERVADA','FINALIZADA')
              AND r.INICIO_TS < SYSTIMESTAMP
              AND r.FIN_TS    > SYSTIMESTAMP
         );
      END;
    ]',
    start_date      => SYSTIMESTAMP,
    repeat_interval => 'FREQ=MINUTELY;INTERVAL=5',  -- Cada 5 minutos
    enabled         => TRUE
  );
END;
/
