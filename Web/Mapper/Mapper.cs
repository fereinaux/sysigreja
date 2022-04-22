<<<<<<< HEAD
﻿using AutoMapper;
using Core.Models.Carona;
using Core.Models.Equipantes;
using Core.Models.Etiquetas;
using Core.Models.Eventos;
=======
﻿using Arquitetura.ViewModels;
using AutoMapper;
using Core.Business.Arquivos;
using Core.Business.ContaBancaria;
using Core.Business.Equipantes;
using Core.Business.Equipes;
using Core.Business.Eventos;
using Core.Business.Lancamento;
using Core.Business.MeioPagamento;
using Core.Business.Reunioes;
using Core.Models.Equipantes;
using Core.Models.Eventos;
using Core.Models.Lancamento;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
using Core.Models.Participantes;
using Core.Models.Quartos;
using Data.Entities;
using SysIgreja.ViewModels;
using System;
<<<<<<< HEAD
using System.Linq;
=======
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Utils.Constants;
using Utils.Enums;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
using Utils.Extensions;
using Utils.Services;

namespace SysIgreja.Controllers
{

    public class MapperRealidade
    {

        public IMapper mapper;

<<<<<<< HEAD
        public MapperRealidade(int? qtdReunioes = null, int? eventoId = null)
        {
            var configuration = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<Equipante, PostEquipanteModel>()
                .ForMember(dest => dest.EtiquetasList, opt => opt.MapFrom(x => x.ParticipantesEtiquetas.Select(y => y.Etiqueta)))
                .ForMember(dest => dest.Foto, opt => opt.MapFrom(x => x.Arquivos.Any(y => y.IsFoto) ? Convert.ToBase64String(x.Arquivos.FirstOrDefault(y => y.IsFoto).Conteudo) : ""));
                cfg.CreateMap<Carona, PostCaronaModel>().ForMember(dest => dest.Motorista, opt => opt.MapFrom(x => x.Motorista.Nome));
                cfg.CreateMap<Quarto, PostQuartoModel>();
                cfg.CreateMap<Evento, PostEventoModel>();
                cfg.CreateMap<Participante, ParticipanteSelectModel>();
                cfg.CreateMap<Etiqueta, PostEtiquetaModel>();
                cfg.CreateMap<Participante, ParticipanteExcelViewModel>()
           .ForMember(dest => dest.Nome, opt => opt.MapFrom(x => UtilServices.CapitalizarNome(x.Nome)))
          .ForMember(dest => dest.Apelido, opt => opt.MapFrom(x => UtilServices.CapitalizarNome(x.Apelido)))
            .ForMember(dest => dest.Idade, opt => opt.MapFrom(x => UtilServices.GetAge(x.DataNascimento)))
            .ForMember(dest => dest.DataNascimento, opt => opt.MapFrom(x => x.DataNascimento.HasValue ? x.DataNascimento.Value.ToString("dd/MM/yyyy") : ""))
            .ForMember(dest => dest.Sexo, opt => opt.MapFrom(x => x.Sexo.GetDescription()))
            .ForMember(dest => dest.Situacao, opt => opt.MapFrom(x => x.Status.GetDescription()));
=======
        public MapperRealidade(int? qtdReunioes = null)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
              
                cfg.CreateMap<Equipante, PostEquipanteModel>().ForMember(dest => dest.Foto, opt => opt.MapFrom(x => x.Arquivos.Any(y => y.IsFoto) ? Convert.ToBase64String(x.Arquivos.FirstOrDefault(y => y.IsFoto).Conteudo) : ""));
                cfg.CreateMap<Quarto, PostQuartoModel>();
                cfg.CreateMap<Evento, PostEventoModel>();
                cfg.CreateMap<Participante, ParticipanteSelectModel>();
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
                cfg.CreateMap<Participante, ParticipanteListModel>()
                    .ForMember(dest => dest.Idade, opt => opt.MapFrom(x => UtilServices.GetAge(x.DataNascimento)))
                    .ForMember(dest => dest.QtdAnexos, opt => opt.MapFrom(x => x.Arquivos.Count()))
                    .ForMember(dest => dest.HasFoto, opt => opt.MapFrom(x => x.Arquivos.Any(y => y.IsFoto)))
                    .ForMember(dest => dest.HasContact, opt => opt.MapFrom(x => x.MsgFoto || x.MsgGeral || x.MsgVacina || x.MsgPagamento || !string.IsNullOrEmpty(x.Observacao)))
                    .ForMember(dest => dest.Sexo, opt => opt.MapFrom(x => x.Sexo.GetDescription()))
                    .ForMember(dest => dest.Padrinho, opt => opt.MapFrom(x => x.PadrinhoId.HasValue ? x.Padrinho.Nome : null))
                    .ForMember(dest => dest.Circulo, opt => opt.MapFrom(x => x.Circulos.Any() ? x.Circulos.LastOrDefault().Circulo.Cor.GetDescription() : ""))
<<<<<<< HEAD
                    .ForMember(dest => dest.Etiquetas, opt => opt.MapFrom(x => x.ParticipantesEtiquetas.Select(y => y.Etiqueta)))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(x => x.Status.GetDescription()));
                cfg.CreateMap<Equipante, EquipanteListModel>()
                .ForMember(dest => dest.Etiquetas, opt => opt.MapFrom(x => x.ParticipantesEtiquetas.Select(y => y.Etiqueta)))
                        .ForMember(dest => dest.Idade, opt => opt.MapFrom(x => UtilServices.GetAge(x.DataNascimento)))
                                .ForMember(dest => dest.Sexo, opt => opt.MapFrom(x => x.Sexo.GetDescription()))
                                     .ForMember(dest => dest.HasFoto, opt => opt.MapFrom(x => x.Arquivos.Any(y => y.IsFoto)))
                                .ForMember(dest => dest.QtdAnexos, opt => opt.MapFrom(x => x.Arquivos.Count()))
                                .ForMember(dest => dest.HasOferta, opt => opt.MapFrom(x => x.Lancamentos.Any(y => y.EventoId == (eventoId ?? x.Equipes.LastOrDefault().EventoId))))
                                .ForMember(dest => dest.Faltas, opt => opt.MapFrom(x => qtdReunioes - x.Equipes.LastOrDefault().Presencas.Count()))
                                .ForMember(dest => dest.Status, opt => opt.MapFrom(x => x.Status.GetDescription()))
                                .ForMember(dest => dest.Equipe, opt => opt.MapFrom(x => (x.Equipes.Any() && x.Equipes.LastOrDefault().EventoId == eventoId) ? x.Equipes.LastOrDefault().Equipe.GetDescription() : null));
                cfg.CreateMap<Equipante, EquipanteExcelModel>()
                  .ForMember(dest => dest.Nome, opt => opt.MapFrom(x => UtilServices.CapitalizarNome(x.Nome)))
                 .ForMember(dest => dest.Apelido, opt => opt.MapFrom(x => UtilServices.CapitalizarNome(x.Apelido)))
                   .ForMember(dest => dest.Idade, opt => opt.MapFrom(x => UtilServices.GetAge(x.DataNascimento)))
                   .ForMember(dest => dest.Sexo, opt => opt.MapFrom(x => x.Sexo.GetDescription()))
                   .ForMember(dest => dest.Status, opt => opt.MapFrom(x => x.Status.GetDescription()))
                   .ForMember(dest => dest.HasVacina, opt => opt.MapFrom(x => x.HasVacina ? "Sim" : "Não"))
                   .ForMember(dest => dest.HasOferta, opt => opt.MapFrom(x => x.Lancamentos.Any(y => y.EventoId == (eventoId ?? x.Equipes.LastOrDefault().EventoId)) ? "Sim" : "Não"))
                   .ForMember(dest => dest.Equipe, opt => opt.MapFrom(x => x.Equipes.LastOrDefault().EventoId == eventoId ? x.Equipes.LastOrDefault().Equipe.GetDescription() : null));
=======
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(x => x.Status.GetDescription()));
                cfg.CreateMap<Equipante, EquipanteListModel>()
                    .ForMember(dest => dest.Idade, opt => opt.MapFrom(x => UtilServices.GetAge(x.DataNascimento)))
                    .ForMember(dest => dest.Sexo, opt => opt.MapFrom(x => x.Sexo.GetDescription()))
                    .ForMember(dest => dest.Sexo, opt => opt.MapFrom(x => x.Sexo.GetDescription()))
                    .ForMember(dest => dest.QtdAnexos, opt => opt.MapFrom(x => x.Arquivos.Count()))
                    .ForMember(dest => dest.Faltas, opt => opt.MapFrom(x => qtdReunioes - x.Equipes.LastOrDefault().Presencas.Count()))
                    .ForMember(dest => dest.HasFoto, opt => opt.MapFrom(x => x.Arquivos.Any(y => y.IsFoto)))
                    .ForMember(dest => dest.Equipe, opt => opt.MapFrom(x => x.Equipes.Any() ? x.Equipes.LastOrDefault().Equipe.GetDescription() : null));
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566


            });
            mapper = configuration.CreateMapper();
        }
    }

<<<<<<< HEAD
}
=======
}
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
